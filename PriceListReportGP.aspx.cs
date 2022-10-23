using BAL;
using DAL;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Production_Costing_Software
{
    public partial class PriceListReportGP : BasePage
    {
        DBHelper dbhelper = new DBHelper();
        CommonDAL common = new CommonDAL();
        PriceListGPActualEstimateDAL plgp = new PriceListGPActualEstimateDAL();
        PriceListGPActualEstimateBAL plgpdata = new PriceListGPActualEstimateBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddata();
            }
        }
        private void binddata()
        {

            DataTable dt = plgp.GetPriceListReportGP(Common.ConvertInt(Session["CompanyId"]));
            gvpricelistgp.DataSource = dt;
            gvpricelistgp.DataBind();
            if (dt.Rows.Count > 0)
            {

                gvpricelistgp.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvpricelistgp.UseAccessibleHeader = true;
            }

        }
        protected void ViewReport_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Report')", true);

        }
        protected void btnReport_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            string MergeList = Common.ConvertString(btn.CommandArgument);
            //ClsCompanyMaster cls = new ClsCompanyMaster();
            //ProCompanyMaster pro = new ProCompanyMaster();
            DataTable dtPriceList = new DataTable();



            string PriceList = MergeList.Substring(0, MergeList.IndexOf('('));
            string StateId = MergeList.Split('(', ')')[1];
            dtPriceList = plgp.GetPriceListReportGPData(PriceList, StateId);
            //PriceList_Report.DataSource = dtPriceList;
            //PriceList_Report.DataBind();

            //if (!Directory.Exists(Server.MapPath("~/TempUse")))
            //{
            //    Directory.CreateDirectory(Server.MapPath("~/TempUse"));
            //}

            //if (File.Exists(Server.MapPath("~/TempUse/PriceList.pdf")))
            //{
            //    File.Delete(Server.MapPath("~/TempUse/PriceList.pdf"));
            //}
            //if (lblCompanyMasterList_Id.Text == "1" || lblCompanyMasterList_Id.Text == "0" || lblCompanyMasterList_Id.Text == "")
            //{
            //    using (var stream = new FileStream(Server.MapPath("~/TempUse/PriceList.pdf"), FileMode.Create))
            //    {
            //        Document document = new Document(PageSize.A4, 5, 5, 15, 5);
            //        PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
            //        document.Open();
            //        iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 10);
            //        iTextSharp.text.Font font10Bold = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 10, 1);
            //        iTextSharp.text.Font font14Bold = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 14, 1);

            //        PdfPTable tableHeader = new PdfPTable(2);
            //        float[] Headerwidths = new float[] { 50f, 50f };
            //        tableHeader.SetWidths(Headerwidths);
            //        tableHeader.WidthPercentage = 100;
            //        PdfPCell cell = new PdfPCell(new Phrase("Price List Report of " + dtPriceList.Rows[0]["CompanyName"], font14Bold));
            //        cell.HorizontalAlignment = 1;
            //        cell.Colspan = 2;
            //        cell.Border = 0;
            //        cell.PaddingBottom = 10;
            //        tableHeader.AddCell(cell);

            //        cell = new PdfPCell(new Phrase("Price list Name : " + dtPriceList.Rows[0]["PriceListName"], font10Bold));
            //        cell.HorizontalAlignment = 3;
            //        cell.Border = 0;
            //        cell.PaddingBottom = 10;
            //        tableHeader.AddCell(cell);

            //        cell = new PdfPCell(new Phrase("State : " + dtPriceList.Rows[0]["StateName"], font10Bold));
            //        cell.HorizontalAlignment = 2;
            //        cell.Border = 0;
            //        cell.PaddingBottom = 10;
            //        tableHeader.AddCell(cell);

            //        cell = new PdfPCell(new Phrase("As on date	 : " + dtPriceList.Rows[0]["AsOnDate"], font10Bold));
            //        cell.HorizontalAlignment = 3;
            //        cell.Border = 0;
            //        cell.PaddingBottom = 10;
            //        tableHeader.AddCell(cell);

            //        cell = new PdfPCell(new Phrase(" ", font10Bold));
            //        cell.HorizontalAlignment = 1;
            //        cell.Border = 0;
            //        cell.PaddingBottom = 10;
            //        tableHeader.AddCell(cell);

            //        PdfPTable table = new PdfPTable(11);
            //        float[] widths = new float[] { 4, 20, 12, 8, 8, 8, 8, 8, 8, 8, 8 };

            //        table.SetWidths(widths);

            //        table.WidthPercentage = 100;
            //        cell = new PdfPCell(new Phrase("No", font10Bold));
            //        cell.Rowspan = 2;
            //        cell.HorizontalAlignment = 1;
            //        cell.VerticalAlignment = ((int)VerticalAlign.Middle);
            //        table.AddCell(cell);

            //        cell = new PdfPCell(new Phrase("Product Name with Technical Name", font10Bold));
            //        cell.Rowspan = 2;
            //        cell.HorizontalAlignment = 1;
            //        cell.VerticalAlignment = 1;
            //        table.AddCell(cell);

            //        cell = new PdfPCell(new Phrase("Category Name", font10Bold));
            //        cell.Rowspan = 2;
            //        cell.HorizontalAlignment = 1;
            //        cell.VerticalAlignment = 1;
            //        table.AddCell(cell);

            //        cell = new PdfPCell(new Phrase("Packing Size", font10Bold));
            //        cell.Rowspan = 2;
            //        cell.HorizontalAlignment = 1;
            //        cell.VerticalAlignment = 1;
            //        table.AddCell(cell);


            //        cell = new PdfPCell(new Phrase("NCR", font10Bold));
            //        cell.Colspan = 3;
            //        cell.HorizontalAlignment = 1;
            //        cell.VerticalAlignment = 1;
            //        table.AddCell(cell);
            //        cell = new PdfPCell(new Phrase("RPL", font10Bold));
            //        cell.HorizontalAlignment = 1;
            //        cell.VerticalAlignment = 1;
            //        cell.Colspan = 3;
            //        table.AddCell(cell);

            //        cell = new PdfPCell(new Phrase("MRP", font10Bold));
            //        cell.Rowspan = 2;
            //        cell.HorizontalAlignment = 1;
            //        cell.VerticalAlignment = 1;
            //        table.AddCell(cell);

            //        table.AddCell(new Phrase("Price/ L or KG", font10Bold));
            //        table.AddCell(new Phrase("Price Per Unit", font10Bold));
            //        table.AddCell(new Phrase("Unit Price with GST", font10Bold));
            //        table.AddCell(new Phrase("Price/ L or KG", font10Bold));
            //        table.AddCell(new Phrase("Price Per Unit", font10Bold));
            //        table.AddCell(new Phrase("Unit Price with GST", font10Bold));

            //        int iCounter = 1;
            //        int fk_BPM_Id = 0;
            //        foreach (DataRow dr in dtPriceList.Rows)
            //        {
            //            int Currentfk_BPM_Id = Convert.ToInt32(dr["fk_BPM_Id"].ToString());
            //            if (fk_BPM_Id != Currentfk_BPM_Id)
            //            {
            //                fk_BPM_Id = Convert.ToInt32(dr["fk_BPM_Id"].ToString());
            //                int Count = dtPriceList.AsEnumerable().Where(r => r.Field<int>("fk_BPM_Id") == Convert.ToInt32(dr["fk_BPM_Id"].ToString())).Count();

            //                cell = new PdfPCell(new Phrase(iCounter.ToString(), font5));
            //                cell.Rowspan = Count;
            //                cell.HorizontalAlignment = 1;
            //                cell.VerticalAlignment = 1;
            //                table.AddCell(cell);

            //                cell = new PdfPCell(new Phrase(dr["BPM_Product_Name"].ToString(), font5));
            //                cell.Rowspan = Count;
            //                cell.HorizontalAlignment = 1;
            //                cell.VerticalAlignment = 1;
            //                table.AddCell(cell);

            //                cell = new PdfPCell(new Phrase(dr["ProductCategoryName"].ToString(), font5));
            //                cell.Rowspan = Count;
            //                cell.HorizontalAlignment = 1;
            //                cell.VerticalAlignment = 1;
            //                table.AddCell(cell);
            //                iCounter++;
            //            }
            //            table.AddCell(new Phrase(dr["PackMeasure"].ToString(), font5));

            //            table.AddCell(new Phrase(dr["NCR"].ToString().Split('|')[0], font5));
            //            table.AddCell(new Phrase(dr["NCR"].ToString().Split('|')[1], font5));
            //            table.AddCell(new Phrase(dr["NCR"].ToString().Split('|')[2], font5));
            //            table.AddCell(new Phrase(dr["RPL"].ToString().Split('|')[0], font5));
            //            table.AddCell(new Phrase(dr["RPL"].ToString().Split('|')[1], font5));
            //            table.AddCell(new Phrase(dr["RPL"].ToString().Split('|')[2], font5));

            //            table.AddCell(new Phrase(dr["MRP"].ToString(), font5));

            //        }


            //        document.Add(tableHeader);
            //        //document.Add(tableFooter);
            //        document.Add(table);
            //        document.Close();

            //    }
            //}
            //else
            //{
            //    using (var stream = new FileStream(Server.MapPath("~/TempUse/PriceList.pdf"), FileMode.Create))
            //    {
            //        Document document = new Document(PageSize.A4, 5, 5, 15, 5);
            //        PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
            //        document.Open();
            //        iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 10);
            //        iTextSharp.text.Font font10Bold = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 10, 1);
            //        iTextSharp.text.Font font14Bold = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 14, 1);

            //        PdfPTable tableHeader = new PdfPTable(2);
            //        float[] Headerwidths = new float[] { 50f, 50f };
            //        tableHeader.SetWidths(Headerwidths);
            //        tableHeader.WidthPercentage = 100;
            //        PdfPCell cell = new PdfPCell(new Phrase("Price List Report of " + dtPriceList.Rows[0]["CompanyName"], font14Bold));
            //        cell.HorizontalAlignment = 1;
            //        cell.Colspan = 2;
            //        cell.Border = 0;
            //        cell.PaddingBottom = 10;
            //        tableHeader.AddCell(cell);

            //        cell = new PdfPCell(new Phrase("Price list Name : " + dtPriceList.Rows[0]["PriceListName"], font10Bold));
            //        cell.HorizontalAlignment = 3;
            //        cell.Border = 0;
            //        cell.PaddingBottom = 10;
            //        tableHeader.AddCell(cell);

            //        cell = new PdfPCell(new Phrase("As on date	 : " + dtPriceList.Rows[0]["AsOnDate"], font10Bold));
            //        cell.HorizontalAlignment = 3;
            //        cell.Border = 0;
            //        cell.PaddingBottom = 10;
            //        tableHeader.AddCell(cell);

            //        cell = new PdfPCell(new Phrase(" ", font10Bold));
            //        cell.HorizontalAlignment = 1;
            //        cell.Border = 0;
            //        cell.PaddingBottom = 10;
            //        tableHeader.AddCell(cell);

            //        PdfPTable table = new PdfPTable(8);
            //        float[] widths = new float[] { 4, 20, 12, 8, 8, 8, 8, 8 };

            //        table.SetWidths(widths);

            //        table.WidthPercentage = 100;
            //        cell = new PdfPCell(new Phrase("No", font10Bold));

            //        cell.HorizontalAlignment = 1;
            //        cell.VerticalAlignment = ((int)VerticalAlign.Middle);
            //        table.AddCell(cell);

            //        cell = new PdfPCell(new Phrase("Product Name with Technical Name", font10Bold));
            //        cell.HorizontalAlignment = 1;
            //        cell.VerticalAlignment = 1;
            //        table.AddCell(cell);

            //        cell = new PdfPCell(new Phrase("Category Name", font10Bold));

            //        cell.HorizontalAlignment = 1;
            //        cell.VerticalAlignment = 1;
            //        table.AddCell(cell);

            //        cell = new PdfPCell(new Phrase("Packing Size", font10Bold));

            //        cell.HorizontalAlignment = 1;
            //        cell.VerticalAlignment = 1;
            //        table.AddCell(cell);



            //        table.AddCell(new Phrase("Price/ L or KG", font10Bold));
            //        table.AddCell(new Phrase("Price Per Unit", font10Bold));
            //        table.AddCell(new Phrase("Unit Price with GST", font10Bold));

            //        cell = new PdfPCell(new Phrase("MRP", font10Bold));
            //        cell.HorizontalAlignment = 1;
            //        cell.VerticalAlignment = 1;
            //        table.AddCell(cell);

            //        int iCounter = 1;
            //        int fk_BPM_Id = 0;
            //        foreach (DataRow dr in dtPriceList.Rows)
            //        {
            //            int Currentfk_BPM_Id = Convert.ToInt32(dr["fk_BPM_Id"].ToString());
            //            if (fk_BPM_Id != Currentfk_BPM_Id)
            //            {
            //                fk_BPM_Id = Convert.ToInt32(dr["fk_BPM_Id"].ToString());
            //                int Count = dtPriceList.AsEnumerable().Where(r => r.Field<int>("fk_BPM_Id") == Convert.ToInt32(dr["fk_BPM_Id"].ToString())).Count();

            //                cell = new PdfPCell(new Phrase(iCounter.ToString(), font5));
            //                cell.Rowspan = Count;
            //                cell.HorizontalAlignment = 1;
            //                cell.VerticalAlignment = 1;
            //                table.AddCell(cell);

            //                cell = new PdfPCell(new Phrase(dr["BPM_Product_Name"].ToString(), font5));
            //                cell.Rowspan = Count;
            //                cell.HorizontalAlignment = 1;
            //                cell.VerticalAlignment = 1;
            //                table.AddCell(cell);

            //                cell = new PdfPCell(new Phrase(dr["ProductCategoryName"].ToString(), font5));
            //                cell.Rowspan = Count;
            //                cell.HorizontalAlignment = 1;
            //                cell.VerticalAlignment = 1;
            //                table.AddCell(cell);
            //                iCounter++;
            //            }
            //            table.AddCell(new Phrase(dr["PackMeasure"].ToString(), font5));

            //            table.AddCell(new Phrase(dr["FinalPrice"].ToString().Split('|')[0], font5));
            //            table.AddCell(new Phrase(dr["FinalPrice"].ToString().Split('|')[1], font5));
            //            table.AddCell(new Phrase(dr["FinalPrice"].ToString().Split('|')[2], font5));

            //            table.AddCell(new Phrase(dr["MRP"].ToString(), font5));

            //        }


            //        document.Add(tableHeader);
            //        //document.Add(tableFooter);
            //        document.Add(table);
            //        document.Close();

            //    }

            //}
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=PriceListGPEstimate.pdf");

            //Response.TransmitFile(Server.MapPath("~/TempUse/PriceList.pdf"));
            //Response.End();

        }
    }
}