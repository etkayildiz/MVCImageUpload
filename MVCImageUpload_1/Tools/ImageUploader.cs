using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MVCImageUpload_1.Tools
{
    public static class ImageUploader
    {
        //Geriye string değer döndüren metodumuz resmin yolunu döndürecek veya resim yükleme ile ilgili bir sorun varsa onun kodunu döndürecek "1","2","3","C/Images/"


        //HttpPostedFileBase => MVC'de eğer Post yönteminde bir dosya geliyorsa bu dosya, HttpPostedFileBase tipinde tutulur.
        public static string UploadImage(string serverPath, HttpPostedFileBase file)
        {

            if (file != null)
            {
                Guid uniqueName = Guid.NewGuid();
                string[] fileArray = file.FileName.Split('.'); //burada Split metodu sayesinde ilgili yapının uzantısının da içeride bulunduğu bir string dizisi almış olduk. Split metodu belirttiğiniz char karakterinden metni bölerek size bir array sunar...
                string extension = fileArray[fileArray.Length - 1].ToLower();//dosya uzantısını yakalayarak küçük harflere çevirdik.

                string fileName = $"{uniqueName}.{extension}"; //normal şartlarda biz burada Guid kullandığımız için asla bir dosya ismi aynı olmayacaktır. Lakin siz Guid kullanmazsanız (kullanıcıya yüklemek istediği dosyanın ismini girdirebilirsiniz).. Böyle bir durum söz konusu ise ek olarak bir kontrol yapmanız gerekir.. Bunu öncelikle extension'ı kontrol edip sonra yapmalısınız...
                if (extension == "jpg" || extension == "gif" || extension == "png" || extension == "jpeg")
                {
                    //Eğer dosya ismi zaten varsa
                    if (File.Exists(HttpContext.Current.Server.MapPath(serverPath + fileName)))
                    {
                        return "1"; //Ancak Guid kullandığımız için bu açıdan zaten güvendeyiz. (Dosya zaten var kodu)
                    }
                    else
                    {
                        string filePath = HttpContext.Current.Server.MapPath(serverPath + fileName);
                        file.SaveAs(filePath);
                        return serverPath + fileName;
                    }
                }
                else
                {
                    return "2";
                }
            }
            else
            {
                return "3";//Dosya boş kodu
            }
            
        }
    }
}