using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reactive.Linq;
using Aksio.Integration;
using AutoMapper;
using Cratis.Changes;
using Cratis.Events.Projections;
using ObjectsComparer;

namespace Integration.AccountHolders
{
    /*
    3rd Party <-> Input Connector -> Data -> Adapter -> Events -> EventLog


    EventLog -> Adapter -> Changes -> Output Connector -> 3rd Party


    Things to consider:

    - Key on the model. Becomes the event source id (Upgrade Dolittle!)
    - Partial data coming - "partial input connector" - can return changeset
      - "WebHook" with changes
    - Parameters
        -
    - "Something triggers"
        - Schedule
        - Manual
            - Often single

    - PersonRegistrert has happened in another microservice -> translate to trigger

        https://dev.azure.com/aksio/OPensjon/_git/Connectors?path=/KontaktOgReservasjonsregisteret/KRR_Klient/IKontaktOgReservasjonsregisteretKlient.cs

#if false
        public void Perform(OppdaterBestandData job)
        {
            // var allePersoner = _db.FetchByOtherId("TenantId", evt.KasseId);
            // foreach (KRRPerson krrPerson in client.HentPersoner(allePersoner.Select(_ => _.PersonId)))
            // {
            //     krrPerson.EmbeddingsTypeLogikkForÅEmitteEventer(); // AdapterInputBuilderFor ting?
            // }
        }

        public void Perform(HentFregOppdateringer job)
        {
            // var fregBestandReferanse = _db.GetbyId(evt.BestandId);

            // Hent endringer i registrert bestand fra folkeregisteret
            // List<BestandChangeset> bestandsEndringer = _fregKlient.HentBestandsOppdateringer(fregBestandReferanse);
            // foreach (var endretPerson in bestandsEndringer){
            //      // endretPerson er da endringene for en person
            //      var eksisterendePerson =_db.HentPerson(endretPerson.Id);
            //      // finn ut hva endringen faktisk er mellom våre registrerte data og det som kom fra FREG; publiser events for å reflektere
            //      eksisterendePerson.DiffOgPubliserEvents(endretPerson);
            // }
        }

        public class KRRPerson
        {
        }
#endif

    */
    public class AccountHolderDetailsAdapter : AdapterFor<AccountHolder, ExternalAccountHolder>
    {
        public override Expression<Func<ExternalAccountHolder, EventSourceId>> Key => _ => _.Id;

        public override void DefineModel(IProjectionBuilderFor<AccountHolder> builder) => builder
            .From<AccountHolderRegistered>(_ => _
                .Set(m => m.FirstName).To(ev => ev.FirstName)
                .Set(m => m.LastName).To(ev => ev.LastName)
                .Set(m => m.DateOfBirth).To(ev => ev.DateOfBirth))
            .From<AccountHolderAddressChanged>(_ => _
                .Set(m => m.Address).To(ev => ev.Address)
                .Set(m => m.City).To(ev => ev.City)
                .Set(m => m.PostalCode).To(ev => ev.PostalCode)
                .Set(m => m.Country).To(ev => ev.Country));

        public override void DefineImport(IImportBuilderFor<AccountHolder, ExternalAccountHolder> builder)
        {
            builder
                .WithProperties(_ => _.FirstName, _ => _.LastName, _ => _.DateOfBirth)
                .AppendEvent(_ => new AccountHolderRegistered(_.Changeset.Incoming.FirstName, _.Changeset.Incoming.LastName, _.Changeset.Incoming.DateOfBirth))
                .AppendEvent<AccountHolder, ExternalAccountHolder, AccountHolderRegistered>();

            builder
                .WithProperties(_ => _.Address, _ => _.City, _ => _.PostalCode)
                .AppendEvent<AccountHolder, ExternalAccountHolder, AccountHolderAddressChanged>();
        }

        public override void DefineImportMapping(IMappingExpression<ExternalAccountHolder, AccountHolder> builder) => builder
            .ForMember(_ => _.FirstName, _ => _.MapFrom(_ => _.FirstName))
            .ForMember(_ => _.LastName, _ => _.MapFrom(_ => _.LastName));
    }
}
