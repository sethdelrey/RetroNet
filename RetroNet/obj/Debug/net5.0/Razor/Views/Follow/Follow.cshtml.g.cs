#pragma checksum "/Users/sethrichard/Documents/GitHub/RetroNet/RetroNet/Views/Follow/Follow.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "10930c61fbcf04a6ab4a52da264c290ecf488566"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Follow_Follow), @"mvc.1.0.view", @"/Views/Follow/Follow.cshtml")]
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
#line 1 "/Users/sethrichard/Documents/GitHub/RetroNet/RetroNet/Views/_ViewImports.cshtml"
using _90sTest;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/sethrichard/Documents/GitHub/RetroNet/RetroNet/Views/_ViewImports.cshtml"
using _90sTest.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"10930c61fbcf04a6ab4a52da264c290ecf488566", @"/Views/Follow/Follow.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1173e3b68e76680577b5ff76455ff62cea7caf0b", @"/Views/_ViewImports.cshtml")]
    public class Views_Follow_Follow : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "/Users/sethrichard/Documents/GitHub/RetroNet/RetroNet/Views/Follow/Follow.cshtml"
  
    ViewData["Title"] = "Follows";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<div class=""container"" style=""margin-top:30px;"">
    <div class=""row"">
        <div class=""col-sm-3"" id=""sidenav"">
            <a href=""#"">
                <button class=""btn mr-2 mb-2 btn-primary border-dark sidenavbutton"" type=""button"">
                    <span class=""btn-text"">Feed</span>
                </button>
            </a>
            <br>
            <a href=""#"">
                <button class=""btn mr-2 mb-2 btn-primary border-dark sidenavbutton"" type=""button"">
                    <span class=""btn-text"">Friends</span>
                </button>
            </a>
            <br>
            <a href=""#"">
                <button class=""btn mr-2 mb-2 btn-primary border-dark sidenavbutton"" type=""button"">
                    <span class=""btn-text"">Profile</span>
                </button>
            </a>
            <br>
            <a href=""#"">
                <button class=""btn mr-2 mb-2 btn-primary border-dark sidenavbutton"" type=""button"">
                    <span class=""btn-text"">Settings</span>
 ");
            WriteLiteral(@"               </button>
            </a>
        </div>
        <div class=""col-sm-9 mb-4 mb-lg-0 feed"">
            <div class=""card"">
                <div class=""card-header"">
                    Following
                </div>
                <div class=""card-body"">
                    <div class=""accordion"" id=""accordionExample"">
                        <div class=""accordion-item"">
                            <h2 class=""accordion-header"" id=""headingOne"">
                                <button class=""accordion-button"" type=""button"" data-bs-toggle=""collapse"" data-bs-target=""#collapseOne"" aria-expanded=""true"" aria-controls=""collapseOne"">
                                    Jim Bob
                                </button>
                            </h2>
                            <div id=""collapseOne"" class=""accordion-collapse collapse show"" aria-labelledby=""headingOne"" data-bs-parent=""#accordionExample"">
                                <div class=""accordion-body"">
                                    T");
            WriteLiteral(@"his is a test div
                                </div>
                            </div>
                        </div>
                        <div class=""accordion-item"">
                            <h2 class=""accordion-header"" id=""headingOne"">
                                <button class=""accordion-button"" type=""button"" data-bs-toggle=""collapse"" data-bs-target=""#collapseOne"" aria-expanded=""true"" aria-controls=""collapseOne"">
                                    Jim Bob
                                </button>
                            </h2>
                            <div id=""collapseOne"" class=""accordion-collapse collapse show"" aria-labelledby=""headingOne"" data-bs-parent=""#accordionExample"">
                                <div class=""accordion-body"">
                                    This is a test div
                                </div>
                            </div>
                        </div>
                        <div class=""accordion-item"">
                            <h2 class=""");
            WriteLiteral(@"accordion-header"" id=""headingOne"">
                                <button class=""accordion-button"" type=""button"" data-bs-toggle=""collapse"" data-bs-target=""#collapseOne"" aria-expanded=""true"" aria-controls=""collapseOne"">
                                    Jim Bob
                                </button>
                            </h2>
                            <div id=""collapseOne"" class=""accordion-collapse collapse show"" aria-labelledby=""headingOne"" data-bs-parent=""#accordionExample"">
                                <div class=""accordion-body"">
                                    This is a test div
                                </div>
                            </div>
                        </div>
                        <div class=""accordion-item"">
                            <h2 class=""accordion-header"" id=""headingOne"">
                                <button class=""accordion-button"" type=""button"" data-bs-toggle=""collapse"" data-bs-target=""#collapseOne"" aria-expanded=""true"" aria-controls=""collapseO");
            WriteLiteral(@"ne"">
                                    Jim Bob
                                </button>
                            </h2>
                            <div id=""collapseOne"" class=""accordion-collapse collapse show"" aria-labelledby=""headingOne"" data-bs-parent=""#accordionExample"">
                                <div class=""accordion-body"">
                                    This is a test div
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591