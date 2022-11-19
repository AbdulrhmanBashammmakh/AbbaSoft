using AbbaSoft.Dts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AbbaSoft
{
    public partial class Invoices : System.Web.UI.Page
    {
       // public int x;
       public int x = Generator.gen();
       
        public DateTime Date1= DateTime.Now;
        private readonly AbabEntities db=new AbabEntities();
        public List<detial> detials=new List<detial>();
     //   public List<product> products = new List<product>();

       public void insert_To_Db()
        {
            var idOrder = x;
            var dateCreated = Date1;
            var tot = TextBox5.Text;
            var typePay = DropDownList2.Text;
            Response.Write(tot + "-----" + dateCreated + " "+"ok"+ "  "+ typePay+ "___"+ idOrder);


            double sum = 0.0;
            foreach (detial d in detials)
            {

                sum = (double)(sum + d.amount);

                //  Response.Write("<br/>"+ d.idinv +"<br/>");
            }

            //  Response.Write("<br/>" + "the total :"+ sum + "<br/>");
            Invoice invoice = new Invoice();
            invoice.id = x;
            invoice.dateInv = dateCreated;
            invoice.total=sum;
            invoice.type="cash";
            invoice.customerName = "no one";

            db.Invoices.Add(invoice);
            db.SaveChanges();
            getAsyncInsert();


        }

        private  void getAsyncInsert()
        {
             db.detials.AddRange(detials);
             db.SaveChanges();
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
          
            
            // x = Generator.gen();
            if (!this.IsPostBack)
            {
               
                SetInitial();
              //  products = db.products.ToList();
               
            }
        }
        private void SetInitial()
        {
           
            DataTable dt = new DataTable();

            DataRow dr = null;

            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));

            dt.Columns.Add(new DataColumn("Product", typeof(string)));
            dt.Columns.Add(new DataColumn("rate", typeof(string)));
            dt.Columns.Add(new DataColumn("qty", typeof(string)));
            dt.Columns.Add(new DataColumn("amount", typeof(string)));
            dt.Columns.Add(new DataColumn("order", typeof(string)));
         

            dr = dt.NewRow();

            dr["RowNumber"] = 1;

            dr["Product"] = string.Empty;

         

            dr["rate"] = string.Empty;
            dr["qty"] = 0.ToString();
            dr["amount"] = 0.ToString();
            dr["order"] = x.ToString();



            dt.Rows.Add(dr);


            ViewState["CurrentTable"] = dt;



            GridView1.DataSource = dt;

            GridView1.DataBind();
        }
        private void AddNewRowToGrid()

        {

            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)

            {

                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)

                {

                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)

                    {

                        //extract the TextBox values
                        DropDownList drop1 = (DropDownList)GridView1.Rows[rowIndex].Cells[1].FindControl("DropDownList1");

                        TextBox box1 = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("TextBox1");

                        DropDownList drop2 = (DropDownList)GridView1.Rows[rowIndex].Cells[3].FindControl("TextBox2");

                        TextBox box3 = (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("TextBox3");
                        TextBox box4 = (TextBox)GridView1.Rows[rowIndex].Cells[5].FindControl("TextBox4");
                   //     TextBox box5 = (TextBox)GridView1.Rows[rowIndex].Cells[6].FindControl("TextBox5");


                        drCurrentRow = dtCurrentTable.NewRow();

                        drCurrentRow["RowNumber"] = i + 1;
                        drCurrentRow["qty"] = 0;
                        drCurrentRow["amount"] = 0;
                        drCurrentRow["order"] = x.ToString();

                        dtCurrentTable.Rows[i - 1]["Product"] = drop1.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["rate"] = drop2.Text;
                        dtCurrentTable.Rows[i - 1]["qty"] = box3.Text;
                        dtCurrentTable.Rows[i - 1]["amount"] = box4.Text;
                        dtCurrentTable.Rows[i - 1]["order"] = x.ToString();
                       
                       

                        rowIndex++;

                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);

                    ViewState["CurrentTable"] = dtCurrentTable;



                    GridView1.DataSource = dtCurrentTable;

                    GridView1.DataBind();

                }

            }

            else

            {

                Response.Write("ViewState is null");

            }



            //Set Previous Data on Postbacks

            SetPreviousData();

        }

        private void SetPreviousData()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)

            {

                DataTable dt = (DataTable)ViewState["CurrentTable"];

                if (dt.Rows.Count > 0)

                {

                    for (int i = 0; i < dt.Rows.Count; i++)

                    {
                        DropDownList drop1 = (DropDownList)GridView1.Rows[rowIndex].Cells[1].FindControl("DropDownList1");

                        TextBox box1 = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("TextBox1");

                        DropDownList drop2 = (DropDownList)GridView1.Rows[rowIndex].Cells[3].FindControl("TextBox2");

                        TextBox box3 = (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("TextBox3");
                        TextBox box4 = (TextBox)GridView1.Rows[rowIndex].Cells[5].FindControl("TextBox4");
                        //     TextBox box5 = (TextBox)GridView1.Rows[rowIndex].Cells[6].FindControl("TextBox5");



                        drop1.Text = dt.Rows[i]["Product"].ToString();

                        box1.Text = dt.Rows[i]["order"].ToString();

                        drop2.Text = dt.Rows[i]["rate"].ToString();

                        box3.Text = dt.Rows[i]["qty"].ToString();
                        box4.Text = dt.Rows[i]["amount"].ToString();


                        rowIndex++;

                    }

                }

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
          
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Response.Write("okay");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            detials.Clear();

            checked1();
            insert_To_Db();


        }

        public void checked1()
        {
            
            if (ViewState["CurrentTable"] != null)

            {

                DataTable dt = (DataTable)ViewState["CurrentTable"];

                if (dt.Rows.Count > 0)

                {

                    for (int i =0; i < dt.Rows.Count; i++)

                    {
                       
                                //    Response.Write("||"+dt.Rows.Count + "||");
                                double amo = Convert.ToDouble(dt.Rows[i]["amount"]);
                                if (amo > 0)
                                {
                                    // Response.Write(amo + "-"+ i+ "|");
                                    detial detl = new detial();
                                    detl.amount = amo;
                                    detl.qtyinv = Convert.ToInt32(dt.Rows[i]["qty"]);
                                    if (dt.Rows[i]["rate"] == null || dt.Rows[i]["rate"].Equals(" "))
                                    {
                                        detl.rate = 0;
                                    }
                                    else
                                    {
                                        detl.rate = Convert.ToInt32(dt.Rows[i]["rate"]);
                                    }
                                    detl.idpro = Convert.ToInt32(dt.Rows[i]["Product"]); ;
                                    detl.idinv = x;
                                    // Response.Write(detl.idpro);

                                    detials.Add(detl);
                                }
                            }

                    }

                }

            }
        }

    
    }
    

        