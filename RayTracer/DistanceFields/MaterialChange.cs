﻿namespace RayTracer.DistanceFields {
    public class MaterialChange : DistanceField {
        public DistanceField Field { get; set; }
        public MaterialSettings MaterialSettings { get; set; }

        public MaterialChange(DistanceField field, MaterialSettings materialSettings) {
            Field = field;
            MaterialSettings = materialSettings;
        }

        public override SampleResult Sample(Vec3 pos) {
            var result = Field.Sample(pos);
            result.Roughness = MaterialSettings.Roughness;
            result.Color = MaterialSettings.GetColor(pos);
            result.Reflectance = MaterialSettings.Reflectance;
            result.Source = MaterialSettings.Source;
            result.Absorbance = MaterialSettings.Absorbance;
            return result;
        }
    }
}
