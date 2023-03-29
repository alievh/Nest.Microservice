using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Nest.Shared.DTO_s
{
    public class ErrorDto
    {
        [AllowNull]
        public List<string> Errors { get; set; }
    }
}