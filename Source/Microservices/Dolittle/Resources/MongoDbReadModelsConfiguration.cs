// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Aksio.Dolittle.Resources
{
    /// <summary>
    /// Represents a <see cref="ResourceConfiguration"/> for MongoDB Read Models.
    /// </summary>
    /// <param name="Host">Host to connect to.</param>
    /// <param name="Database">Database to use.</param>
    /// <param name="UseSSL">Whether or not to use SSL for connecting.</param>
    public record MongoDbReadModelsConfiguration(string Host, string Database, bool UseSSL) : ResourceConfiguration;
}
