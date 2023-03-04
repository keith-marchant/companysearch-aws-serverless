using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CompanySearch.Core.UnitTests")]

// The Mock proxy factory generates a dynamic assembly called "DynamicProxyGenAssembly2" which
// needs access to internals. This entry is per Mock documentation. The alternative is strongly
// named assemblies.
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
