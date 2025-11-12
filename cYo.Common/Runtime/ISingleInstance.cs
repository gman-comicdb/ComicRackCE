#if NET10_0_OR_GREATER
using System;

namespace cYo.Common.Runtime;

internal interface ISingleInstance : IDisposable
{
    void InvokeLast(string[] args);
}
#else
using System.ServiceModel;

namespace cYo.Common.Runtime
{
	[ServiceContract]
	internal interface ISingleInstance
	{
		[OperationContract]
		void InvokeLast(string[] args);
	}
}
#endif