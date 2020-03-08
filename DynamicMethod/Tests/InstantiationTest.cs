using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace DynamicMethod.Tests
{
	public class InstantiationTest : PerformanceTest
	{
		private delegate object ConstructorDelegate();

		public InstantiationTest() : base("Instantiation", "", 1_000_000)
		{
		}

		protected override bool MeasureTestA()
		{
			var type = Type.GetType("System.Text.StringBuilder");
			for (int i = 0; i < Iterations; i++)
			{
				var obj = Activator.CreateInstance(type);
				if(obj.GetType() != typeof(System.Text.StringBuilder))
				{
					throw new Exception("Obj is not a Stringbuilder");
				}
			}

			return true;
		}

		protected override bool MeasureTestB()
		{
			var constructor = GetConstructor("System.Text.StringBuilder");
			for (int i = 0; i < Iterations; i++)
			{
				var obj = constructor();
				if (obj.GetType() != typeof(System.Text.StringBuilder))
				{
					throw new Exception("Obj is not a Stringbuilder");
				}
			}

			return true;
		}

		protected override bool MeasureTestC()
		{
			for (int i = 0; i < Iterations; i++)
			{
				var obj = new StringBuilder();
				if (obj.GetType() != typeof(System.Text.StringBuilder))
				{
					throw new Exception("Obj is not a Stringbuilder");
				}
			}

			return true;
		}

		private ConstructorDelegate GetConstructor(string typeName)
		{
			Type t = Type.GetType(typeName);
			ConstructorInfo ctor = t.GetConstructor(new Type[0]);

			var methodName = t.Name + "Ctor";
			System.Reflection.Emit.DynamicMethod dm = new System.Reflection.Emit.DynamicMethod(methodName, t, new Type[0], typeof(Activator));
			ILGenerator lgen = dm.GetILGenerator();
			lgen.Emit(OpCodes.Newobj, ctor);
			lgen.Emit(OpCodes.Ret);

			ConstructorDelegate creator = (ConstructorDelegate)dm.CreateDelegate(typeof(ConstructorDelegate));

			return creator;
		}
	}
}
