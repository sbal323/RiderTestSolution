using System.IO;
using Microsoft.ML;
using Microsoft.ML.Core.Data;

namespace AiTest
{
    public class Utility
    {
        public static void SaveModelAsFile(MLContext mlContext, ITransformer model, string path)
        {
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
                mlContext.Model.Save(model,fs);
        }
    }
}