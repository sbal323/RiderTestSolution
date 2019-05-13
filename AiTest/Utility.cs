using System.IO;
using Microsoft.ML;

namespace AiTest
{
    public class Utility
    {
        public static void SaveModelAsFile(MLContext mlContext, ITransformer model, IDataView view, string path)
        {
            //using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
            mlContext.Model.Save(model,view.Schema, path);
        }
    }
}