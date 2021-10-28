try {
    const fs = require('fs');
    const https = require('https');
    const path = require('path');

    const file = path.join(__dirname, 'Directory.Build.props');

    console.log('Updating Aksio NuGet package references.');
    console.log(`Working on file ${file}`);

    const content = fs.readFileSync(file).toString();

    const packages = [
        "Aksio.Defaults",
        "Aksio.Microservices",
        "Aksio.ProxyGenerator"
    ];

    async function handlePackages() {
        let result = content;
        const promises = [];

        for (const package of packages) {
            console.log(`Handle package ${package}`);

            const options = {
                hostname: 'azuresearch-usnc.nuget.org',
                port: 443,
                path: `/query\?q\=${package}\&prerelease\=false\&semVerLevel\=2.0.0`,
                method: 'GET'
            };

            promises.push(new Promise((resolve) => {
                const req = https.request(options, res => {
                    let body = '';
                    res.on('data', _ => {
                        body += _;
                    });

                    res.on('end', () => {
                        const searchResult = JSON.parse(body);
                        const version = searchResult.data[0].version;
                        console.log(`Setting '${package}' to version '${version}' based on latest version on NuGet.`);

                        const expression = `(?<=<PackageReference Include="${package}" Version=")[\\w\\.\\-]+(?=")`;
                        const regex = new RegExp(expression, 'g');
                        result = result.replace(regex, version);

                        resolve();
                    });
                });

                req.end();
            }));
        }

        await Promise.all(promises);

        fs.writeFileSync(file, result);
    }

    (async () => await handlePackages())();
} catch (ex) {
    console.error(ex);
    throw ex;
}