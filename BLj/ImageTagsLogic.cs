using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEj;
using DALj;

namespace BLj
{
    public class ImageTagsLogic
    {
        public List<string> GetTags(string path)
        {
            List<string> Result = new List<string>();

            ImageDetails DrugImage = new ImageDetails(path);

            ImageAnalysis dal = new ImageAnalysis();

            dal.GetTags(DrugImage);

            var Threshold = 60.0;

            foreach (var item in DrugImage.Details)
            {
                if (item.Value > Threshold)
                {
                    Result.Add(item.Key);
                }
                else
                {
                    break;
                }

            }

            return Result;
        }
    }
}