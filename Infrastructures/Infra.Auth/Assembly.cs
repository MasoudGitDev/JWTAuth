
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Tests.Infra.Auth")]

namespace Infra.Auth;
internal class Assembly {
    internal static System.Reflection.Assembly GetExecutingAssembly() {
        throw new NotImplementedException();
    }
}
