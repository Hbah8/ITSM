#pragma checksum "D:\Workstation\repos\ITSM\WebApplication1\Views\Dashboard\Assign.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f84ce5630186f9373904d6c175406b33bb6bc87c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Dashboard_Assign), @"mvc.1.0.view", @"/Views/Dashboard/Assign.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Workstation\repos\ITSM\WebApplication1\Views\_ViewImports.cshtml"
using WebApplication1;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Workstation\repos\ITSM\WebApplication1\Views\_ViewImports.cshtml"
using WebApplication1.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f84ce5630186f9373904d6c175406b33bb6bc87c", @"/Views/Dashboard/Assign.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"729efaa87342638aecfe1a972ce9f9f8dff55b4c", @"/Views/_ViewImports.cshtml")]
    public class Views_Dashboard_Assign : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Specialist>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Workstation\repos\ITSM\WebApplication1\Views\Dashboard\Assign.cshtml"
  
    Layout = "AdminPageLayout";
    int counter = 1;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <div class=\"row pb-5 mb-4\">\r\n\r\n");
#nullable restore
#line 10 "D:\Workstation\repos\ITSM\WebApplication1\Views\Dashboard\Assign.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""col-lg-3 col-md-6 mb-4 mb-lg-0"">
                <!-- Card-->
                <div class=""card rounded shadow-sm border-0"">
                    <div class=""card-body p-0"">
                        <div class=""bg-primary px-5 py-4 text-center card-img-top"">
                            <img src=""https://d19m59y37dris4.cloudfront.net/university/1-1-1/img/teacher-4.jpg"" alt=""..."" width=""100"" class=""rounded-circle mb-2 img-thumbnail d-block mx-auto"">
                            <h5 class=""text-white mb-0"">");
#nullable restore
#line 18 "D:\Workstation\repos\ITSM\WebApplication1\Views\Dashboard\Assign.cshtml"
                                                   Write(item.Profile?.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                            <p class=\"small text-white mb-0\">");
#nullable restore
#line 19 "D:\Workstation\repos\ITSM\WebApplication1\Views\Dashboard\Assign.cshtml"
                                                        Write(item.Role?.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 24 "D:\Workstation\repos\ITSM\WebApplication1\Views\Dashboard\Assign.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Specialist>> Html { get; private set; }
    }
}
#pragma warning restore 1591
