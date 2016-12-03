using System;
using System.Linq.Expressions;
using RayTracer;
using RayTracer.DistanceFields;
using Newtonsoft.Json;
using Zhucai.LambdaParser;

public class Program {
    public static void Main() {
        try {
            var df = DistanceField.LoadFromJson(@"d:/test_scene.json");

            Console.WriteLine(JsonConvert.SerializeObject(df, Formatting.Indented));

            var fn = ExpressionParser.Compile<Func<Vec3, Vec3>>(@"(Vec3 pos) => new Vec3(1.0D, 2.0, 3.0)", "System", "RayTracer");
        } catch (Exception err) {
            Console.WriteLine(err.StackTrace);
        }
        Console.ReadKey();
    }
}