#pragma checksum "C:\Users\emma\source\repos\AuctionSite1\AuctionSite1\Views\Auctions\_FindAuctions.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "17e8e94b5a8a97a7a381fe91efbba815181ffcab"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Auctions__FindAuctions), @"mvc.1.0.view", @"/Views/Auctions/_FindAuctions.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Auctions/_FindAuctions.cshtml", typeof(AspNetCore.Views_Auctions__FindAuctions))]
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
#line 1 "C:\Users\emma\source\repos\AuctionSite1\AuctionSite1\Views\_ViewImports.cshtml"
using AuctionSite1;

#line default
#line hidden
#line 2 "C:\Users\emma\source\repos\AuctionSite1\AuctionSite1\Views\_ViewImports.cshtml"
using AuctionSite1.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"17e8e94b5a8a97a7a381fe91efbba815181ffcab", @"/Views/Auctions/_FindAuctions.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e1d79d9c6ad87b49f6169e5c57796f7145415c84", @"/Views/_ViewImports.cshtml")]
    public class Views_Auctions__FindAuctions : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<AuctionSite1.Models.AuktionValues>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Auctions", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Auctions", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(55, 8, true);
            WriteLiteral("\r\n<ul>\r\n");
            EndContext();
#line 5 "C:\Users\emma\source\repos\AuctionSite1\AuctionSite1\Views\Auctions\_FindAuctions.cshtml"
     foreach (var item in Model)
	{
		

#line default
#line hidden
#line 7 "C:\Users\emma\source\repos\AuctionSite1\AuctionSite1\Views\Auctions\_FindAuctions.cshtml"
         if (item.Stängd == "Öppen")
		{


#line default
#line hidden
            BeginContext(169, 192, true);
            WriteLiteral("\t\t\t<li class=\"list-group-item bg-light mb-3 float-left ml-2\">\r\n\t\t\t\t<div class=\"card-header bg-light\">\r\n\t\t\t\t\t<h5>Aktuell</h5>\r\n\t\t\t\t</div>\r\n\t\t\t\t<div class=\"card-body bg-light\">\r\n\t\t\t\t\t<h6>Titel: ");
            EndContext();
            BeginContext(362, 10, false);
#line 15 "C:\Users\emma\source\repos\AuctionSite1\AuctionSite1\Views\Auctions\_FindAuctions.cshtml"
                          Write(item.Titel);

#line default
#line hidden
            EndContext();
            BeginContext(372, 21, true);
            WriteLiteral("</h6>\r\n\t\t\t\t\t<h6>Bud: ");
            EndContext();
            BeginContext(394, 16, false);
#line 16 "C:\Users\emma\source\repos\AuctionSite1\AuctionSite1\Views\Auctions\_FindAuctions.cshtml"
                        Write(item.HögstaSumma);

#line default
#line hidden
            EndContext();
            BeginContext(410, 30, true);
            WriteLiteral(":-</h6>\r\n\t\t\t\t\t<h6>Utropspris: ");
            EndContext();
            BeginContext(441, 15, false);
#line 17 "C:\Users\emma\source\repos\AuctionSite1\AuctionSite1\Views\Auctions\_FindAuctions.cshtml"
                               Write(item.Utropspris);

#line default
#line hidden
            EndContext();
            BeginContext(456, 14, true);
            WriteLiteral(":-</h6>\r\n\t\t\t\t\t");
            EndContext();
            BeginContext(470, 98, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "66c503cc6d1d4317a31105ff2a51548e", async() => {
                BeginContext(552, 12, true);
                WriteLiteral("Visa auktion");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 18 "C:\Users\emma\source\repos\AuctionSite1\AuctionSite1\Views\Auctions\_FindAuctions.cshtml"
                                                                         WriteLiteral(item.AuktionID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(568, 24, true);
            WriteLiteral("\r\n\t\t\t\t</div>\r\n\t\t\t</li>\r\n");
            EndContext();
#line 21 "C:\Users\emma\source\repos\AuctionSite1\AuctionSite1\Views\Auctions\_FindAuctions.cshtml"
		}
		else
		{

#line default
#line hidden
            BeginContext(610, 207, true);
            WriteLiteral("\t\t\t<li class=\"list-group-item bg-dark float-left ml-2\">\r\n\t\t\t\t<div class=\"card-header text-white bg-dark \">\r\n\t\t\t\t\t<h5>Stängd</h5>\r\n\t\t\t\t</div>\r\n\t\t\t\t<div class=\"card-body text-white bg-dark \">\r\n\t\t\t\t\t<h6>Titel: ");
            EndContext();
            BeginContext(818, 10, false);
#line 29 "C:\Users\emma\source\repos\AuctionSite1\AuctionSite1\Views\Auctions\_FindAuctions.cshtml"
                          Write(item.Titel);

#line default
#line hidden
            EndContext();
            BeginContext(828, 29, true);
            WriteLiteral("</h6>\r\n\t\t\t\t\t<h6>Slut priset: ");
            EndContext();
            BeginContext(858, 16, false);
#line 30 "C:\Users\emma\source\repos\AuctionSite1\AuctionSite1\Views\Auctions\_FindAuctions.cshtml"
                                Write(item.HögstaSumma);

#line default
#line hidden
            EndContext();
            BeginContext(874, 12, true);
            WriteLiteral("</h6>\r\n\t\t\t\t\t");
            EndContext();
            BeginContext(886, 98, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "beb505d89a4a48a795adc6c2930f7737", async() => {
                BeginContext(968, 12, true);
                WriteLiteral("Visa auktion");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 31 "C:\Users\emma\source\repos\AuctionSite1\AuctionSite1\Views\Auctions\_FindAuctions.cshtml"
                                                                         WriteLiteral(item.AuktionID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(984, 24, true);
            WriteLiteral("\r\n\t\t\t\t</div>\r\n\t\t\t</li>\r\n");
            EndContext();
#line 34 "C:\Users\emma\source\repos\AuctionSite1\AuctionSite1\Views\Auctions\_FindAuctions.cshtml"
		}

#line default
#line hidden
#line 34 "C:\Users\emma\source\repos\AuctionSite1\AuctionSite1\Views\Auctions\_FindAuctions.cshtml"
         
	}

#line default
#line hidden
            BeginContext(1017, 5, true);
            WriteLiteral("</ul>");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<AuctionSite1.Models.AuktionValues>> Html { get; private set; }
    }
}
#pragma warning restore 1591
