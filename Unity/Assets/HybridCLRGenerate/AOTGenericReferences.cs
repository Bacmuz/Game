using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"System.Core.dll",
		"System.dll",
		"Unity.Mono.dll",
		"Unity.ThirdParty.dll",
		"mscorlib.dll",
	};
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// ET.ETAsyncTaskMethodBuilder<ET.WaitType.Wait_CreateMyUnit>
	// ET.ETAsyncTaskMethodBuilder<ET.WaitType.Wait_SceneChangeFinish>
	// ET.ETAsyncTaskMethodBuilder<ET.WaitType.Wait_UnitStop>
	// ET.ETAsyncTaskMethodBuilder<byte>
	// ET.ETAsyncTaskMethodBuilder<int>
	// ET.ETAsyncTaskMethodBuilder<object>
	// ET.ETTask<ET.WaitType.Wait_CreateMyUnit>
	// ET.ETTask<ET.WaitType.Wait_SceneChangeFinish>
	// ET.ETTask<ET.WaitType.Wait_UnitStop>
	// ET.ETTask<byte>
	// ET.ETTask<int>
	// ET.ETTask<object>
	// ET.ListComponent<UnityEngine.Vector3>
	// ET.ListComponent<object>
	// ET.MultiMap<long,ET.CoroutineLockTimer>
	// ET.MultiMap<long,long>
	// ET.UnOrderMultiMap<object,object>
	// System.Action<byte>
	// System.Action<long,int>
	// System.Action<long,object>
	// System.Action<object,object>
	// System.Action<object>
	// System.Collections.Generic.Dictionary.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.Enumerator<long,object>
	// System.Collections.Generic.Dictionary.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection<object,object>
	// System.Collections.Generic.Dictionary<int,ET.RpcInfo>
	// System.Collections.Generic.Dictionary<int,long>
	// System.Collections.Generic.Dictionary<int,object>
	// System.Collections.Generic.Dictionary<long,object>
	// System.Collections.Generic.Dictionary<object,int>
	// System.Collections.Generic.Dictionary<object,long>
	// System.Collections.Generic.Dictionary<object,object>
	// System.Collections.Generic.Dictionary<object,ushort>
	// System.Collections.Generic.Dictionary<ushort,object>
	// System.Collections.Generic.HashSet.Enumerator<object>
	// System.Collections.Generic.HashSet<object>
	// System.Collections.Generic.HashSet<ushort>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.KeyValuePair<int,object>
	// System.Collections.Generic.KeyValuePair<long,object>
	// System.Collections.Generic.KeyValuePair<object,int>
	// System.Collections.Generic.KeyValuePair<object,object>
	// System.Collections.Generic.List.Enumerator<UnityEngine.Vector3>
	// System.Collections.Generic.List.Enumerator<long>
	// System.Collections.Generic.List.Enumerator<object>
	// System.Collections.Generic.List<ET.CoroutineLockTimer>
	// System.Collections.Generic.List<UnityEngine.Vector3>
	// System.Collections.Generic.List<float>
	// System.Collections.Generic.List<int>
	// System.Collections.Generic.List<long>
	// System.Collections.Generic.List<object>
	// System.Collections.Generic.Queue<ET.CoroutineLockInfo>
	// System.Collections.Generic.Queue<ET.CoroutineLockTimer>
	// System.Collections.Generic.Queue<System.ValueTuple<int,long,int>>
	// System.Collections.Generic.Queue<long>
	// System.Collections.Generic.Queue<object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<int,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<int,object>
	// System.Collections.Generic.SortedDictionary<int,object>
	// System.Collections.Generic.SortedDictionary<long,object>
	// System.Func<System.Collections.Generic.KeyValuePair<object,int>,int>
	// System.Func<long,object,byte>
	// System.Func<object,object>
	// System.ValueTuple<int,long,int>
	// System.ValueTuple<ushort,object>
	// }}

	public void RefMethods()
	{
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,object>(System.Runtime.CompilerServices.TaskAwaiter&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.WaitType.Wait_CreateMyUnit>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.WaitType.Wait_SceneChangeFinish>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.WaitType.Wait_UnitStop>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,object>(object&,object&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.WaitType.Wait_CreateMyUnit>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.WaitType.Wait_SceneChangeFinish>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<ET.WaitType.Wait_UnitStop>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<int>.Start<object>(object&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<object>(object&)
		// System.Void ET.ForeachHelper.ForEachFunc<long,ET.CoroutineLockTimer>(ET.MultiMap<long,ET.CoroutineLockTimer>,System.Func<long,System.Collections.Generic.List<ET.CoroutineLockTimer>,bool>)
		// System.Void ET.ForeachHelper.ForEachFunc<long,long>(ET.MultiMap<long,long>,System.Func<long,System.Collections.Generic.List<long>,bool>)
		// System.Void ET.ForeachHelper.Foreach<long,object>(System.Collections.Generic.Dictionary<long,object>,System.Action<long,object>)
		// System.Void ET.ForeachHelper.Foreach<object,object>(System.Collections.Generic.Dictionary<object,object>,System.Action<object,object>)
		// System.Void ET.ForeachHelper.Foreach<object>(System.Collections.Generic.HashSet<object>,System.Action<object>)
		// System.Void ET.ObjectHelper.Swap<object>(object&,object&)
		// string ET.StringHelper.ArrayToString<float>(float[])
		// System.Void ProtoBuf.Serializer.Serialize<object>(System.IO.Stream,object)
		// ET.WaitType.Wait_CreateMyUnit System.Activator.CreateInstance<ET.WaitType.Wait_CreateMyUnit>()
		// ET.WaitType.Wait_SceneChangeFinish System.Activator.CreateInstance<ET.WaitType.Wait_SceneChangeFinish>()
		// ET.WaitType.Wait_UnitStop System.Activator.CreateInstance<ET.WaitType.Wait_UnitStop>()
		// System.Linq.IOrderedEnumerable<System.Collections.Generic.KeyValuePair<object,int>> System.Linq.Enumerable.OrderByDescending<System.Collections.Generic.KeyValuePair<object,int>,int>(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,int>>,System.Func<System.Collections.Generic.KeyValuePair<object,int>,int>)
		// ET.RpcInfo[] System.Linq.Enumerable.ToArray<ET.RpcInfo>(System.Collections.Generic.IEnumerable<ET.RpcInfo>)
		// object[] System.Linq.Enumerable.ToArray<object>(System.Collections.Generic.IEnumerable<object>)
	}
}