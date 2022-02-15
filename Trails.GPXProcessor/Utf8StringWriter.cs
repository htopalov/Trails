using System.Text;

namespace Trails.GPXProcessor
{
    public class Utf8StringWriter : StringWriter
    {
        public Utf8StringWriter(StringBuilder builder)
            : base(builder)
        {
        }

        public override Encoding Encoding => Encoding.UTF8;
    }
}
