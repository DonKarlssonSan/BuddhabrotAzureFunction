#r "System.Drawing"

using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;


public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info($"HTTP request triggered. RequestUri={req.RequestUri}");
 
    var response = new HttpResponseMessage();

    var stopWatch = new Stopwatch();
    stopWatch.Start();
    var width = 300;
    var height = 300;
    var totalIterations = 2000000;
    Random r = new Random();

    using(MemoryStream ms = new MemoryStream()) 
    {
        using (Bitmap bitmap = new Bitmap(width, height))
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Black);
                
                var x = 0.0;
                var y = 0.0;
                var iteration = 0;
                var max_iteration = 80;
                List<Tuple<double, double>> points;
                for (var i = 0; i < totalIterations; i++)
                {
                    var x0 = r.NextDouble() * 4 - 3;
                    var y0 = r.NextDouble() * 4 - 2;
                    x = 0.0;
                    y = 0.0;
                    iteration = 0;
                    points = new List<Tuple<double, double>>();
                    while (x * x + y * y < 4 && iteration < max_iteration)
                    {
                        var xtemp = x * x - y * y + x0;
                        y = 2 * x * y + y0;
                        x = xtemp;
                        var p = Tuple.Create(x, y);
                        points.Add(p);
                        iteration = iteration + 1;
                    }
                    if (x * x + y * y > 4 || iteration > max_iteration)
                    {
                        points.ForEach((point) =>
                        {
                            var xx = (point.Item1 + 2.1) * width/3;
                            var yy = (point.Item2 + 1.4) * height/3;
    
                            if (xx < width && xx > 0 && yy < height && yy > 0)
                            {
                                SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(1, 255, 255, 255));
                                g.FillRectangle(semiTransBrush, (float)yy, (float)xx, 1, 1);
                            }
                        });
                    }
                }
                bitmap.Save(ms, ImageFormat.Png);
            }
            response.Content = new ByteArrayContent(ms.ToArray());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
        }
    }
    
    stopWatch.Stop();
    
    log.Info($"Time elapsed: {stopWatch.Elapsed}.");

    return response;
}    
