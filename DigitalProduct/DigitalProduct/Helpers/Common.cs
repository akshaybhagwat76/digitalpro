using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Drawing;


namespace DigitalProduct.Helpers
{
    public class Common
    {
        public string SaveBase64AsImage(string base64, string imageFolderPath)
        {
            try
            {
                if (base64.IndexOf(".png") == -1)
                {
                    if (!System.IO.Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + imageFolderPath)))
                    {
                        System.IO.Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + imageFolderPath));
                    }
                    var bytes = Convert.FromBase64String(base64);
                    var filename = string.Format("{0:yyyy-MM-dd-H-mm-dd}_{1}.png", DateTime.Now, DateTime.Now.Millisecond);
                    File.WriteAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + imageFolderPath, filename), bytes);
                    return imageFolderPath + "/" + filename;
                }
                else
                {
                    return base64;
                }
            }
            catch (Exception ex)
            {
                return base64;
            }
        }

        public string SaveBase64AsPDF(string base64, string imageFolderPath)
        {
            try
            {
                if (base64.IndexOf(".pdf") == -1)
                {
                    if (!System.IO.Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + imageFolderPath)))
                    {
                        System.IO.Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + imageFolderPath));
                    }
                    var bytes = Convert.FromBase64String(base64);
                    var filename = string.Format("{0:yyyy-MM-dd-H-mm-dd}_{1}.pdf", DateTime.Now, DateTime.Now.Millisecond);
                    File.WriteAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + imageFolderPath, filename), bytes);
                    return imageFolderPath + "/" + filename;
                }
                else
                {
                    return base64;
                }
            }
            catch (Exception ex)
            {
                return base64;
            }
        }

        public DateTime ConvertLocalToUTC(DateTime date, string time_zone)
        {
            if (!string.IsNullOrEmpty(time_zone))
            {
                TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(time_zone);
                return date.TimeOfDay.TotalSeconds == 0 ? date : TimeZoneInfo.ConvertTimeToUtc(date, timeZoneInfo);
            }
            else
            {
                return date;
            }
        }

        public DateTime ConvertUTCToLocal(DateTime date, string time_zone)
        {
            if (!string.IsNullOrEmpty(time_zone))
            {
                TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(time_zone);
                return date.TimeOfDay.TotalSeconds == 0 ? date : TimeZoneInfo.ConvertTimeFromUtc(date, timeZoneInfo);
            }
            else
            {
                return date;
            }
        }

        public string GenerateAvtarImage(String text, string imageFolderPath, string imageName)
        {
            if (!System.IO.Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + imageFolderPath)))
            {
                System.IO.Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + imageFolderPath));
            }
            //first, create a dummy bitmap just to get a graphics object  
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be 
            Font font = new Font(FontFamily.GenericSansSerif, 45, FontStyle.Regular);
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object  
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size  
            img = new Bitmap(120, 120);

            drawing = Graphics.FromImage(img);

            //paint the background 
            Color bgcolor = ColorTranslator.FromHtml(ColorsCode().OrderBy(a => Guid.NewGuid()).FirstOrDefault());
            drawing.Clear(bgcolor);

            //create a brush for the text  
            Color fontcolor = ColorTranslator.FromHtml("#FFF");
            Brush textBrush = new SolidBrush(fontcolor);

            SizeF size = drawing.MeasureString(text, font);

            //drawing.DrawRectangle(new Pen(Color.Red, 1), 0.0F, 0.0F, size.Width, size.Height);
            //drawing.DrawString(text, font, textBrush, new RectangleF(0,0, size.Width, size.Height));
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            drawing.DrawString(text, font, textBrush, (120 - size.Width) / 2, 24);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();
            string filePath = imageFolderPath + "/" + imageName + ".png";
            img.Save(HttpContext.Current.Server.MapPath(filePath));

            return filePath;

        }

        public List<string> ColorsCode()
        {
            List<string> list = new List<string>();
            list.Add("#EEAD0E");
            list.Add("#8bbf61");

            list.Add("#DC143C");
            list.Add("#CD6889");
            list.Add("#8B8386");
            list.Add("#800080");
            list.Add("#9932CC");
            list.Add("#009ACD");
            list.Add("#00CED1");
            list.Add("#03A89E");

            list.Add("#00C78C");
            list.Add("#00CD66");
            list.Add("#66CD00");
            list.Add("#EEB422");
            list.Add("#FF8C00");
            list.Add("#EE4000");

            list.Add("#388E8E");
            list.Add("#8E8E38");
            list.Add("#7171C6");

            return list;
        }
    }
}