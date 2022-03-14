using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Entities.ErrorModel
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        //System.text.json is used instead of newtonsoft
        public override string ToString() => JsonSerializer.Serialize(this);

       
    }
}
