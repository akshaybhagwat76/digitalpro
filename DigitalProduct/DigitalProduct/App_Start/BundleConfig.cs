using System.Web;
using System.Web.Optimization;

namespace DigitalProduct
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // CSS style (bootstrap/inspinia)
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/animate.css",
                      "~/Content/style.css",
                      "~/Content/common.css"));

            // Font Awesome icons
            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                      "~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.1.1.min.js"));

            // jQueryUI CSS
            bundles.Add(new ScriptBundle("~/Scripts/plugins/jquery-ui/jqueryuiStyles").Include(
                        "~/Scripts/plugins/jquery-ui/jquery-ui.min.css"));

            // jQueryUI 
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/plugins/jquery-ui/jquery-ui.min.js"));

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            // dataTables css styles
            bundles.Add(new StyleBundle("~/Content/plugins/dataTables/dataTablesStyles").Include(
                      "~/Content/plugins/dataTables/datatables.min.css",
                      "~/Content/plugins/dataTables/responsive.min.css"));

            // dataTables 
            bundles.Add(new ScriptBundle("~/plugins/dataTables").Include(
                      "~/Scripts/plugins/dataTables/datatables.min.js",
                      "~/Scripts/plugins/dataTables/responsive.min.js"));

            // dataPicker styles
            bundles.Add(new StyleBundle("~/plugins/dataPickerStyles").Include(
                      "~/Content/plugins/datapicker/datepicker3.css"));

            // dataPicker 
            bundles.Add(new ScriptBundle("~/plugins/datePicker").Include(
                      "~/Scripts/plugins/datepicker/bootstrap-datepicker.js"));

            // clockpicker styles
            bundles.Add(new StyleBundle("~/plugins/clockpickerStyles").Include(
                      "~/Content/plugins/clockpicker/clockpicker.css"));

            // clockpicker 
            bundles.Add(new ScriptBundle("~/plugins/clockpicker").Include(
                      "~/Scripts/plugins/clockpicker/clockpicker.js"));

            // summernote styles
            bundles.Add(new StyleBundle("~/plugins/summernoteStyles").Include(
                      "~/Content/plugins/summernote/summernote.css",
                      "~/Content/plugins/summernote/summernote-bs3.css"));

            // summernote 
            bundles.Add(new ScriptBundle("~/plugins/summernote").Include(
                      "~/Scripts/plugins/summernote/summernote.min.js"));

            // switchery styles
            bundles.Add(new StyleBundle("~/plugins/switcheryStyles").Include(
                      "~/Content/plugins/switchery/switchery.css"));

            // switchery 
            bundles.Add(new ScriptBundle("~/plugins/switchery").Include(
                      "~/Scripts/plugins/switchery/switchery.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/inspinia").Include(
                      "~/Scripts/plugins/metisMenu/metisMenu.min.js",
                      "~/Scripts/plugins/pace/pace.min.js",
                      "~/Scripts/app/inspinia.js"));

            // Inspinia skin config script
            bundles.Add(new ScriptBundle("~/bundles/skinConfig").Include(
                      "~/Scripts/app/skin.config.min.js"));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
                      "~/Scripts/plugins/slimscroll/jquery.slimscroll.min.js"));

            // image cropper
            bundles.Add(new ScriptBundle("~/plugins/imagecropper").Include(
                      "~/Scripts/plugins/cropper/cropper.js",
                      "~/Scripts/pdf.js"));

            // image cropper styles
            bundles.Add(new StyleBundle("~/plugins/imagecropperStyles").Include(
                      "~/Content/plugins/cropper/cropper.css"));

            // validate styles
            bundles.Add(new StyleBundle("~/plugins/validateStyles").Include(
                      "~/Content/plugins/validate/validationEngine.jquery.css"));

            // validate
            bundles.Add(new ScriptBundle("~/plugins/validate").Include(
                      "~/Scripts/plugins/validate/jquery.validationEngine-en.js",
                      "~/Scripts/plugins/validate/jquery.validationEngine.js"));

            // toastr notification 
            bundles.Add(new ScriptBundle("~/plugins/toastr").Include(
                      "~/Scripts/plugins/toastr/toastr.min.js"));

            // toastr notification styles
            bundles.Add(new StyleBundle("~/plugins/toastrStyles").Include(
                      "~/Content/plugins/toastr/toastr.min.css"));

            // iCheck css styles
            bundles.Add(new StyleBundle("~/Content/plugins/iCheck/iCheckStyles").Include(
                      "~/Content/plugins/iCheck/custom.css"));

            // iCheck
            bundles.Add(new ScriptBundle("~/plugins/iCheck").Include(
                      "~/Scripts/plugins/iCheck/icheck.min.js"));

            // ddslick
            bundles.Add(new ScriptBundle("~/plugins/ddslick").Include(
                      "~/Scripts/plugins/ddslick/jquery.ddslick.min.js"));

            // Duallist Styless
            bundles.Add(new StyleBundle("~/plugins/duallistStyles").Include(
                      "~/Content/plugins/dualListbox/bootstrap-duallistbox.min.css"));

            // Duallist
            bundles.Add(new ScriptBundle("~/plugins/duallist").Include(
                      "~/Scripts/plugins/dualListbox/jquery.bootstrap-duallistbox.js"));

            // Lightbox gallery css styles
            bundles.Add(new StyleBundle("~/Content/plugins/blueimp/css/").Include(
                      "~/Content/plugins/blueimp/css/blueimp-gallery.min.css"));

            // Lightbox gallery
            bundles.Add(new ScriptBundle("~/plugins/blueimp").Include(
                      "~/Scripts/plugins/blueimp/jquery.blueimp-gallery.min.js"));

            // Touch Spin Styless
            bundles.Add(new StyleBundle("~/plugins/touchSpinStyles").Include(
                      "~/Content/plugins/touchspin/jquery.bootstrap-touchspin.min.css"));

            // Touch Spin
            bundles.Add(new ScriptBundle("~/plugins/touchSpin").Include(
                      "~/Scripts/plugins/touchspin/jquery.bootstrap-touchspin.min.js"));

            // ChartJS chart
            bundles.Add(new ScriptBundle("~/plugins/chartJs").Include(
                      "~/Scripts/plugins/chartjs/Chart.min.js"));

            // Peity
            bundles.Add(new ScriptBundle("~/plugins/peity").Include(
                      "~/Scripts/plugins/peity/jquery.peity.min.js"));

            //Common File Upload
            bundles.Add(new ScriptBundle("~/Assets/CommonFileUpload").Include(
                     "~/Assets/CommonFileUpload.js"));

            //Common JS
            bundles.Add(new ScriptBundle("~/Assets/Common").Include(
                     "~/Assets/common.js"));

            // Login 
            bundles.Add(new ScriptBundle("~/Assets/Login").Include(
                      "~/Assets/common.js",
                      "~/Assets/Login.js"));

            // Products
            bundles.Add(new ScriptBundle("~/Assets/Products").Include(
                      "~/Assets/Product.js"));

            //Edit Product
            bundles.Add(new ScriptBundle("~/Assets/EditProduct").Include(
                      "~/Assets/EditProduct.js"));

            // Categories
            bundles.Add(new ScriptBundle("~/Assets/Categories").Include(
                      "~/Assets/Categories.js"));

            //Edit Category
            bundles.Add(new ScriptBundle("~/Assets/EditCategory").Include(
                      "~/Assets/EditCategory.js"));
        }
    }
}
