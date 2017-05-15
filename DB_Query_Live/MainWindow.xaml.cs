using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DB_Query_Live
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string conStr_Test = @"Data Source = 10.124.54.86; User ID = engine; Password = engine@5566;";
        string conStr_T1 = @"Data Source = T1-SMT-RealTime\SQLEXPRESS; User ID = engine; Password = engine;";
        string conStr_T2 = @"Data Source = T2-SMT-RealTime\SQLEXPRESS; User ID = engine; Password = engine;";
        string conStr_T3 = @"Data Source = T3-SMT-RealTime\SQLEXPRESS; User ID = engine; Password = engine;";
        string sql = null;
        string sqlt1 = null;
        string sqlt2 = null;
        string sqlt33 = null;
        string sqlt35 = null;
        string dt_min = null;
        string dt_max = null;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dt_min = datetime_min.Text;
            dt_max = datetime_max.Text;
            var sqlx = $"SELECT count(*) as TotalCount FROM [SMTrealTimeT1].[dbo].[PdCount2] where datetimes > '{dt_min}' "
                        + $"SELECT count(*) as TotalCount FROM [SMTrealTimeT2].[dbo].[PdCount2] where datetimes > '{dt_min}' "
                        + $"SELECT count(*) as TotalCount FROM [SMTrealTime1].[dbo].[PdCount2] where datetimes > '{dt_min}' "
                        + $"SELECT count(*) as TotalCount FROM [SMTrealTime3].[dbo].[PdCount2] where datetimes > '{dt_min}' ";

            var resultReclist = QuerSQL_Test(sqlx, conStr_Test);
            Text_PdCount2_T1.Text = resultReclist?.ElementAtOrDefault(0).Rows[0].ItemArray[0].ToString();
            Text_PdCount2_T2.Text = resultReclist?.ElementAtOrDefault(1).Rows[0].ItemArray[0].ToString();
            Text_PdCount2_T3_3.Text = resultReclist?.ElementAtOrDefault(2).Rows[0].ItemArray[0].ToString();
            Text_PdCount2_T3_5.Text = resultReclist?.ElementAtOrDefault(3).Rows[0].ItemArray[0].ToString();

            var sql1t1x = $"SELECT count(*) as TotalCount FROM [SMTrealTime].[dbo].[PdCount2] where datetimes > '{dt_min}' ";
            var sql1t2x = $"SELECT count(*) as TotalCount FROM [SMTrealTime].[dbo].[PdCount2] where datetimes > '{dt_min}' ";
            var sql1t33x = $"SELECT count(*) as TotalCount FROM [SMTrealTime1].[dbo].[PdCount2] where datetimes > '{dt_min}' ";
            var sql1t35x = $"SELECT count(*) as TotalCount FROM [SMTrealTime3].[dbo].[PdCount2] where datetimes > '{dt_min}' ";

            Text1_PdCount2_T1.Text = QuerSQL(sql1t1x, conStr_T1).Rows[0].ItemArray[0].ToString();
            Text1_PdCount2_T2.Text = QuerSQL(sql1t2x, conStr_T2).Rows[0].ItemArray[0].ToString();
            Text1_PdCount2_T3_3.Text = QuerSQL(sql1t33x, conStr_T3).Rows[0].ItemArray[0].ToString();
            Text1_PdCount2_T3_5.Text = QuerSQL(sql1t35x, conStr_T3).Rows[0].ItemArray[0].ToString();
        }


        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            //T1
            dt_min = datetime_min.Text;
            dt_max = datetime_max.Text;
            SQL(dt_min, dt_max);
            var resultReclistt1 = QuerSQL_Test(sqlt1, conStr_Test);
            var resultReclistt1x = QuerSQL_list(sql, conStr_T1);
            var Queryt1 = resultReclistt1?.ElementAtOrDefault(1);
            var Queryt1x = resultReclistt1x?.ElementAtOrDefault(1);
            grid_T1.DataContext = Queryt1.DefaultView;
            grid1_T1.DataContext = Queryt1x.DefaultView;

            //T2
            var resultReclistt2 = QuerSQL_Test(sqlt2, conStr_Test);
            var resultReclistt2x = QuerSQL_list(sql, conStr_T2);
            var Queryt2 = resultReclistt2?.ElementAtOrDefault(1);
            var Queryt2x = resultReclistt2x?.ElementAtOrDefault(1);
            grid_T2.DataContext = Queryt2.DefaultView;
            grid1_T2.DataContext = Queryt2x.DefaultView;

            //T3
            var resultReclistt33 = QuerSQL_Test(sqlt33, conStr_Test);
            var resultReclistt33x = QuerSQL_list(sqlt33, conStr_T3);
            var Queryt33 = resultReclistt33?.ElementAtOrDefault(1);
            var Queryt33x = resultReclistt33x?.ElementAtOrDefault(1);
            grid_T33.DataContext = Queryt33.DefaultView;
            grid1_T33.DataContext = Queryt33x.DefaultView;

            var resultReclistt35 = QuerSQL_Test(sqlt35, conStr_Test);
            var resultReclistt35x = QuerSQL_list(sqlt35, conStr_T3);
            var Queryt35 = resultReclistt35?.ElementAtOrDefault(1);
            var Queryt35x = resultReclistt35x?.ElementAtOrDefault(1);
            grid_T35.DataContext = Queryt35.DefaultView;
            grid1_T35.DataContext = Queryt35x.DefaultView;
        }

        private void SQL(string dtmin, string dtmax)
        {
            sqlt1 = $"SELECT * From(Select count(*) as PDcount2 FROM[SMTrealTimeT1].[dbo].[PdCount2] where datetimes > '{dtmin}') as A, "
            + $" (Select count(*) as RecipeChg FROM[SMTrealTimeT1].[dbo].[RecipeChg] where datetimes > '{dtmin}' ) as B, "
            + $" (Select count(*) as BoardCount FROM[SMTrealTimeT1].[dbo].BoardCount where datetimes > '{dtmin}') as C, "
            + $" (Select count(*) as McStatus FROM[SMTrealTimeT1].[dbo].McStatus where datetimes > '{dtmin}' ) as D,"
            + $" (Select count(*) as ProdStart FROM[SMTrealTimeT1].[dbo].ProdStart where datetimes > '{dtmin}' ) as E,"
            + $" (Select count(*) as ProdEnd FROM[SMTrealTimeT1].[dbo].[ProdEnd] where datetimes > '{dtmin}' ) as F,"
            + $" (Select count(*) as ProdStop FROM[SMTrealTimeT1].[dbo].[ProdStop] where datetimes > '{dtmin}' ) as G,"
            + $" (Select count(*) as PdError FROM[SMTrealTimeT1].[dbo].[PdError] where datetimes > '{dtmin}' ) as H,"
            + $" (Select count(*) as [RecipeDel] FROM[SMTrealTimeT1].[dbo].[RecipeDel] where datetimes > '{dtmin}' ) as I,"
            + $" (Select count(*) as [ConnectOff] FROM[SMTrealTimeT1].[dbo].[ConnectOff] where datetimes > '{dtmin}' ) as J,"
            + $" (Select count(*) as [AAmode] FROM[SMTrealTimeT1].[dbo].[AAmode] where datetimes > '{dtmin}' ) as K"

            + $" SELECT* From(Select count(*) as PDcount2 FROM[SMTrealTimeT1].[dbo].[PdCount2] where datetimes between '{dtmin}'  and '{dtmax}' ) as A,"
            + $" (Select count(*) as RecipeChg FROM[SMTrealTimeT1].[dbo].[RecipeChg] where datetimes between '{dtmin}'  and '{dtmax}' ) as B,"
            + $" (Select count(*) as BoardCount FROM[SMTrealTimeT1].[dbo].BoardCount where datetimes between '{dtmin}'  and '{dtmax}'  ) as C,"
            + $" (Select count(*) as McStatus FROM[SMTrealTimeT1].[dbo].McStatus where datetimes between '{dtmin}'  and '{dtmax}'  ) as D,"
            + $" (Select count(*) as ProdStart FROM[SMTrealTimeT1].[dbo].ProdStart where datetimes between '{dtmin}'  and '{dtmax}' ) as E,"
            + $" (Select count(*) as ProdEnd FROM[SMTrealTimeT1].[dbo].[ProdEnd] where datetimes between '{dtmin}'  and '{dtmax}'  ) as F,"
            + $" (Select count(*) as ProdStop FROM[SMTrealTimeT1].[dbo].[ProdStop] where datetimes between '{dtmin}'  and '{dtmax}'  ) as G,"
            + $" (Select count(*) as PdError FROM[SMTrealTimeT1].[dbo].[PdError] where datetimes between '{dtmin}'  and '{dtmax}'  ) as H,"
            + $" (Select count(*) as [RecipeDel] FROM[SMTrealTimeT1].[dbo].[RecipeDel] where datetimes between '{dtmin}'  and '{dtmax}'  ) as I,"
            + $" (Select count(*) as [ConnectOff] FROM[SMTrealTimeT1].[dbo].[ConnectOff] where datetimes between '{dtmin}'  and '{dtmax}'  ) as J,"
            + $" (Select count(*) as [AAmode] FROM[SMTrealTimeT1].[dbo].[AAmode] where datetimes between '{dtmin}'  and '{dtmax}' ) as K ";

            sql = $"SELECT * From(Select count(*) as PDcount2 FROM[SMTrealTime].[dbo].[PdCount2] where datetimes > '{dtmin}') as A, "
            + $" (Select count(*) as RecipeChg FROM[SMTrealTime].[dbo].[RecipeChg] where datetimes > '{dtmin}' ) as B, "
            + $" (Select count(*) as BoardCount FROM[SMTrealTime].[dbo].BoardCount where datetimes > '{dtmin}') as C, "
            + $" (Select count(*) as McStatus FROM[SMTrealTime].[dbo].McStatus where datetimes > '{dtmin}' ) as D,"
            + $" (Select count(*) as ProdStart FROM[SMTrealTime].[dbo].ProdStart where datetimes > '{dtmin}' ) as E,"
            + $" (Select count(*) as ProdEnd FROM[SMTrealTime].[dbo].[ProdEnd] where datetimes > '{dtmin}' ) as F,"
            + $" (Select count(*) as ProdStop FROM[SMTrealTime].[dbo].[ProdStop] where datetimes > '{dtmin}' ) as G,"
            + $" (Select count(*) as PdError FROM[SMTrealTime].[dbo].[PdError] where datetimes > '{dtmin}' ) as H,"
            + $" (Select count(*) as [RecipeDel] FROM[SMTrealTime].[dbo].[RecipeDel] where datetimes > '{dtmin}' ) as I,"
            + $" (Select count(*) as [ConnectOff] FROM[SMTrealTime].[dbo].[ConnectOff] where datetimes > '{dtmin}' ) as J,"
            + $" (Select count(*) as [AAmode] FROM[SMTrealTime].[dbo].[AAmode] where datetimes > '{dtmin}' ) as K"


            + $"  SELECT* From(Select count(*) as PDcount2 FROM[SMTrealTime].[dbo].[PdCount2] where datetimes between '{dtmin}'  and '{dtmax}') as A,"
            + $" (Select count(*) as RecipeChg FROM[SMTrealTime].[dbo].[RecipeChg] where datetimes between '{dtmin}'  and '{dtmax}') as B,"
            + $" (Select count(*) as BoardCount FROM[SMTrealTime].[dbo].BoardCount where datetimes between '{dtmin}'  and '{dtmax}'  ) as C,"
            + $" (Select count(*) as McStatus FROM[SMTrealTime].[dbo].McStatus where datetimes between '{dtmin}'  and '{dtmax}'  ) as D,"
            + $" (Select count(*) as ProdStart FROM[SMTrealTime].[dbo].ProdStart where datetimes between '{dtmin}'  and '{dtmax}' ) as E,"
            + $" (Select count(*) as ProdEnd FROM[SMTrealTime].[dbo].[ProdEnd] where datetimes between '{dtmin}'  and '{dtmax}' ) as F,"
            + $" (Select count(*) as ProdStop FROM[SMTrealTime].[dbo].[ProdStop] where datetimes between '{dtmin}'  and '{dtmax}'  ) as G,"
            + $" (Select count(*) as PdError FROM[SMTrealTime].[dbo].[PdError] where datetimes between '{dtmin}'  and '{dtmax}' ) as H,"
            + $" (Select count(*) as [RecipeDel] FROM[SMTrealTime].[dbo].[RecipeDel] where datetimes between '{dtmin}'  and '{dtmax}') as I,"
            + $" (Select count(*) as [ConnectOff] FROM[SMTrealTime].[dbo].[ConnectOff] where datetimes between '{dtmin}'  and '{dtmax}') as J,"
            + $" (Select count(*) as [AAmode] FROM[SMTrealTime].[dbo].[AAmode] where datetimes between '{dtmin}'  and '{dtmax}' ) as K ";

            //T2
            sqlt2 = $"SELECT * From(Select count(*) as PDcount2 FROM[SMTrealTimeT2].[dbo].[PdCount2] where datetimes > '{dtmin}') as A, "
            + $" (Select count(*) as RecipeChg FROM[SMTrealTimeT2].[dbo].[RecipeChg] where datetimes > '{dtmin}' ) as B, "
            + $" (Select count(*) as BoardCount FROM[SMTrealTimeT2].[dbo].BoardCount where datetimes > '{dtmin}') as C, "
            + $" (Select count(*) as McStatus FROM[SMTrealTimeT2].[dbo].McStatus where datetimes > '{dtmin}' ) as D,"
            + $" (Select count(*) as ProdStart FROM[SMTrealTimeT2].[dbo].ProdStart where datetimes > '{dtmin}' ) as E,"
            + $" (Select count(*) as ProdEnd FROM[SMTrealTimeT2].[dbo].[ProdEnd] where datetimes > '{dtmin}' ) as F,"
            + $" (Select count(*) as ProdStop FROM[SMTrealTimeT2].[dbo].[ProdStop] where datetimes > '{dtmin}' ) as G,"
            + $" (Select count(*) as PdError FROM[SMTrealTimeT2].[dbo].[PdError] where datetimes > '{dtmin}' ) as H,"
            + $" (Select count(*) as [RecipeDel] FROM[SMTrealTimeT2].[dbo].[RecipeDel] where datetimes > '{dtmin}' ) as I,"
            + $" (Select count(*) as [ConnectOff] FROM[SMTrealTimeT2].[dbo].[ConnectOff] where datetimes > '{dtmin}' ) as J,"
            + $" (Select count(*) as [AAmode] FROM[SMTrealTimeT2].[dbo].[AAmode] where datetimes > '{dtmin}' ) as K"


            + $"  SELECT* From(Select count(*) as PDcount2 FROM[SMTrealTimeT2].[dbo].[PdCount2] where datetimes between '{dtmin}'  and '{dtmax}') as A,"
            + $" (Select count(*) as RecipeChg FROM[SMTrealTimeT2].[dbo].[RecipeChg] where datetimes between '{dtmin}'  and '{dtmax}') as B,"
            + $" (Select count(*) as BoardCount FROM[SMTrealTimeT2].[dbo].BoardCount where datetimes between '{dtmin}'  and '{dtmax}' ) as C,"
            + $" (Select count(*) as McStatus FROM[SMTrealTimeT2].[dbo].McStatus where datetimes between '{dtmin}'  and '{dtmax}' ) as D,"
            + $" (Select count(*) as ProdStart FROM[SMTrealTimeT2].[dbo].ProdStart where datetimes between '{dtmin}'  and '{dtmax}') as E,"
            + $" (Select count(*) as ProdEnd FROM[SMTrealTimeT2].[dbo].[ProdEnd] where datetimes between '{dtmin}'  and '{dtmax}' ) as F,"
            + $" (Select count(*) as ProdStop FROM[SMTrealTimeT2].[dbo].[ProdStop] where datetimes between '{dtmin}'  and '{dtmax}' ) as G,"
            + $" (Select count(*) as PdError FROM[SMTrealTimeT2].[dbo].[PdError] where datetimes between '{dtmin}'  and '{dtmax}') as H,"
            + $" (Select count(*) as [RecipeDel] FROM[SMTrealTimeT2].[dbo].[RecipeDel] where datetimes between '{dtmin}'  and '{dtmax}' ) as I,"
            + $" (Select count(*) as [ConnectOff] FROM[SMTrealTimeT2].[dbo].[ConnectOff] where datetimes between '{dtmin}'  and '{dtmax}') as J,"
            + $" (Select count(*) as [AAmode] FROM[SMTrealTimeT2].[dbo].[AAmode] where datetimes between '{dtmin}'  and '{dtmax}' ) as K ";

            //T3_3
            sqlt33 = $"SELECT * From(Select count(*) as PDcount2 FROM[SMTrealTime1].[dbo].[PdCount2] where datetimes > '{dtmin}') as A, "
            + $" (Select count(*) as RecipeChg FROM[SMTrealTime1].[dbo].[RecipeChg] where datetimes > '{dtmin}' ) as B, "
            + $" (Select count(*) as BoardCount FROM[SMTrealTime1].[dbo].BoardCount where datetimes > '{dtmin}') as C, "
            + $" (Select count(*) as McStatus FROM[SMTrealTime1].[dbo].McStatus where datetimes > '{dtmin}' ) as D,"
            + $" (Select count(*) as ProdStart FROM[SMTrealTime1].[dbo].ProdStart where datetimes > '{dtmin}' ) as E,"
            + $" (Select count(*) as ProdEnd FROM[SMTrealTime1].[dbo].[ProdEnd] where datetimes > '{dtmin}' ) as F,"
            + $" (Select count(*) as ProdStop FROM[SMTrealTime1].[dbo].[ProdStop] where datetimes > '{dtmin}' ) as G,"
            + $" (Select count(*) as PdError FROM[SMTrealTime1].[dbo].[PdError] where datetimes > '{dtmin}' ) as H,"
            + $" (Select count(*) as [RecipeDel] FROM[SMTrealTime1].[dbo].[RecipeDel] where datetimes > '{dtmin}' ) as I,"
            + $" (Select count(*) as [ConnectOff] FROM[SMTrealTime1].[dbo].[ConnectOff] where datetimes > '{dtmin}' ) as J,"
            + $" (Select count(*) as [AAmode] FROM[SMTrealTime1].[dbo].[AAmode] where datetimes > '{dtmin}' ) as K"

                        + $"  SELECT* From(Select count(*) as PDcount2 FROM[SMTrealTime1].[dbo].[PdCount2] where datetimes between '{dtmin}'  and '{dtmax}') as A,"
                        + $" (Select count(*) as RecipeChg FROM[SMTrealTime1].[dbo].[RecipeChg] where datetimes between '{dtmin}'  and '{dtmax}') as B,"
                        + $" (Select count(*) as BoardCount FROM[SMTrealTime1].[dbo].BoardCount where datetimes between '{dtmin}'  and '{dtmax}' ) as C,"
                        + $" (Select count(*) as McStatus FROM[SMTrealTime1].[dbo].McStatus where datetimes between '{dtmin}'  and '{dtmax}') as D,"
                        + $" (Select count(*) as ProdStart FROM[SMTrealTime1].[dbo].ProdStart where datetimes between '{dtmin}'  and '{dtmax}' ) as E,"
                        + $" (Select count(*) as ProdEnd FROM[SMTrealTime1].[dbo].[ProdEnd] where datetimes between '{dtmin}'  and '{dtmax}' ) as F,"
                        + $" (Select count(*) as ProdStop FROM[SMTrealTime1].[dbo].[ProdStop] where datetimes between '{dtmin}'  and '{dtmax}') as G,"
                        + $" (Select count(*) as PdError FROM[SMTrealTime1].[dbo].[PdError] where datetimes between '{dtmin}'  and '{dtmax}') as H,"
                        + $" (Select count(*) as [RecipeDel] FROM[SMTrealTime1].[dbo].[RecipeDel] where datetimes between '{dtmin}'  and '{dtmax}' ) as I,"
                        + $" (Select count(*) as [ConnectOff] FROM[SMTrealTime1].[dbo].[ConnectOff] where datetimes between '{dtmin}'  and '{dtmax}') as J,"
                        + $" (Select count(*) as [AAmode] FROM[SMTrealTime1].[dbo].[AAmode] where datetimes between '{dtmin}'  and '{dtmax}' ) as K ";
            //T3_5
            sqlt35 = $"SELECT * From(Select count(*) as PDcount2 FROM[SMTrealTime3].[dbo].[PdCount2] where datetimes > '{dtmin}') as A, "
            + $" (Select count(*) as RecipeChg FROM[SMTrealTime3].[dbo].[RecipeChg] where datetimes > '{dtmin}' ) as B, "
            + $" (Select count(*) as BoardCount FROM[SMTrealTime3].[dbo].BoardCount where datetimes > '{dtmin}') as C, "
            + $" (Select count(*) as McStatus FROM[SMTrealTime3].[dbo].McStatus where datetimes > '{dtmin}' ) as D,"
            + $" (Select count(*) as ProdStart FROM[SMTrealTime3].[dbo].ProdStart where datetimes > '{dtmin}' ) as E,"
            + $" (Select count(*) as ProdEnd FROM[SMTrealTime3].[dbo].[ProdEnd] where datetimes > '{dtmin}' ) as F,"
            + $" (Select count(*) as ProdStop FROM[SMTrealTime3].[dbo].[ProdStop] where datetimes > '{dtmin}' ) as G,"
            + $" (Select count(*) as PdError FROM[SMTrealTime3].[dbo].[PdError] where datetimes > '{dtmin}' ) as H,"
            + $" (Select count(*) as [RecipeDel] FROM[SMTrealTime3].[dbo].[RecipeDel] where datetimes > '{dtmin}' ) as I,"
            + $" (Select count(*) as [ConnectOff] FROM[SMTrealTime3].[dbo].[ConnectOff] where datetimes > '{dtmin}' ) as J,"
            + $" (Select count(*) as [AAmode] FROM[SMTrealTime3].[dbo].[AAmode] where datetimes > '{dtmin}' ) as K"


            + $"  SELECT* From(Select count(*) as PDcount2 FROM[SMTrealTime3].[dbo].[PdCount2] where datetimes between '{dtmin}'  and '{dtmax}') as A,"
            + $" (Select count(*) as RecipeChg FROM[SMTrealTime3].[dbo].[RecipeChg] where datetimes between '{dtmin}'  and '{dtmax}' ) as B,"
            + $" (Select count(*) as BoardCount FROM[SMTrealTime3].[dbo].BoardCount where datetimes between '{dtmin}'  and '{dtmax}') as C,"
            + $" (Select count(*) as McStatus FROM[SMTrealTime3].[dbo].McStatus where datetimes between '{dtmin}'  and '{dtmax}') as D,"
            + $" (Select count(*) as ProdStart FROM[SMTrealTime3].[dbo].ProdStart where datetimes between '{dtmin}'  and '{dtmax}' ) as E,"
            + $" (Select count(*) as ProdEnd FROM[SMTrealTime3].[dbo].[ProdEnd] where datetimes between '{dtmin}'  and '{dtmax}') as F,"
            + $" (Select count(*) as ProdStop FROM[SMTrealTime3].[dbo].[ProdStop] where datetimes between '{dtmin}'  and '{dtmax}' ) as G,"
            + $" (Select count(*) as PdError FROM[SMTrealTime3].[dbo].[PdError] where datetimes between '{dtmin}'  and '{dtmax}') as H,"
            + $" (Select count(*) as [RecipeDel] FROM[SMTrealTime3].[dbo].[RecipeDel] where datetimes between '{dtmin}'  and '{dtmax}' ) as I,"
            + $" (Select count(*) as [ConnectOff] FROM[SMTrealTime3].[dbo].[ConnectOff] where datetimes between '{dtmin}'  and '{dtmax}') as J,"
            + $" (Select count(*) as [AAmode] FROM[SMTrealTime3].[dbo].[AAmode] where datetimes between '{dtmin}'  and '{dtmax}') as K ";
        }

        private List<DataTable> QuerSQL_Test(string RealTimeDB, string db)
        {
            var dtRetList = new List<DataTable>();
            SqlConnection connection;
            //SqlDataReader dataReader;
            connection = new SqlConnection(db);
            try
            {
                connection.Open();
                DataSet dataSet = new DataSet();
                //使用 SqlDataAdapter 查詢資料，並將結果存回 DataSet 多個的 DataTable
                SqlDataAdapter dataAdapter1 = new SqlDataAdapter(RealTimeDB, connection);
                dataAdapter1.Fill(dataSet);

                foreach (DataTable tbl in dataSet.Tables)
                {
                    dtRetList.Add(tbl);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return dtRetList;
        }

        private List<DataTable> QuerSQL_list(string RealTimeDB, string db)
        {
            var dtRetList = new List<DataTable>();
            SqlConnection connection;
            //SqlDataReader dataReader;
            connection = new SqlConnection(db);
            try
            {
                connection.Open();
                DataSet dataSet = new DataSet();
                //使用 SqlDataAdapter 查詢資料，並將結果存回 DataSet 多個的 DataTable
                SqlDataAdapter dataAdapter1 = new SqlDataAdapter(RealTimeDB, connection);
                dataAdapter1.Fill(dataSet);

                foreach (DataTable tbl in dataSet.Tables)
                {
                    dtRetList.Add(tbl);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return dtRetList;
        }

        private DataTable QuerSQL(string RealTimeDB, string db)
        {
            var dtRet = new DataTable();
            SqlConnection connection;
            SqlCommand command;
            SqlDataReader dataReader;

            connection = new SqlConnection(db);
            try
            {
                connection.Open();
                command = new SqlCommand(RealTimeDB, connection);
                dataReader = command.ExecuteReader();
                dtRet.Load(dataReader);

                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return dtRet;
        }
    }
}
