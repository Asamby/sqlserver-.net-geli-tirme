using sqlDataBase.Atribute;
using sqlDataBase.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sqlDataBase.Geliştirme
{
 public  class SqlCekme<T> : SqlServer where T :class, new()
    {
        Type type = typeof(T);

        ConstructorInfo NesneDelagete;
        T Nesne;
        
        string ModelAdı;


        public SqlCekme()
        {

             ModelAdı = type.Name;
           

        }

        public int fieldSayısı { get
            {
                string sqlStr = $"select * from {ModelAdı} ";
              SqlDataReader  Okuyucu = this.okuma(sqlStr, CommandType.Text);
                
                return Okuyucu.FieldCount; } }

        public string fieldName(int index) 
        {
            string sqlStr = $"select * from {ModelAdı} ";
            SqlDataReader Okuyucu = this.okuma(sqlStr, CommandType.Text);
            Okuyucu.Close();
            return Okuyucu.GetName(index);
        }

        public ArrayList fieldNameGetAll()
        {
            string sqlStr = $"select * from {ModelAdı} ";
            SqlDataReader Okuyucu = this.okuma(sqlStr, CommandType.Text);
           
            ArrayList fieldlar = new ArrayList();
            for (int i = 1; i < fieldSayısı; i++)
            {
                fieldlar.Add(Okuyucu.GetName(i));
            }
            Okuyucu.Close();
            return fieldlar;
        }

        public List<T> Tolist() //bütün veriyi cekiyoz
        {
            string sqlStr = $"select * from {ModelAdı} ";
          SqlDataReader  Okuyucu = this.okuma(sqlStr, CommandType.Text);

            List<T> liste = new List<T>();
  
            while (Okuyucu.Read())
            {

                // burda gelen tipin nesne sini kuruyoz
                NesneDelagete = type.GetConstructor(Type.EmptyTypes);
                // delaget geliyo burda biz delagete kendi türüne cast ediyoz yani kendi türüne ceviriyoz
                Nesne = (T)NesneDelagete.Invoke(null);

                for (int i = 0; i < Okuyucu.FieldCount; i++)
                {
                    // sql den gelen proporti isinleri
                    var SqlProp= Okuyucu.GetName(i);
                   //sınıfın proportisi
                    var properti = type.GetProperty(SqlProp, BindingFlags.Instance | BindingFlags.Public);

                    if (Okuyucu.GetDataTypeName(i) == "int")
                        { 
                        //sqlden gelen değer
                            var sqlPropDeğeri = Okuyucu.GetInt32(i);
                        //propetriye Sqlden cekilen degeri setliyoz
                           properti.SetValue(Nesne, sqlPropDeğeri);  
                        }
                    if (Okuyucu.GetDataTypeName(i) == "nvarchar")
                    {
                        var sqlPropDeğeri = Okuyucu.GetString(i);
                        properti.SetValue(Nesne, sqlPropDeğeri);
                    }

                }
                liste.Add(Nesne);

                //for (int i = 0; i < fieldSayısı-1; i++)
                //{

                //    if(snf.GetDataTypeName(i)=="int")
                //   ob.Add(new T{ I= snf.GetInt32(i) });
                //    if (snf.GetDataTypeName(i) == "nvarchar")
                //     ob.Add(snf.GetString(i));
                //}
                //liste.Add(ob);

            }
           Okuyucu.Close();

           return liste.ToList();
          
        }
        public T Get(string kolondeğeri)
        {

            foreach (var item in fieldNameGetAll())
            {




                // string ModelAdı = type.Name;
                string sqlStr = $"select * from {ModelAdı} where {item}={kolondeğeri}";
                var Okuyucu = this.okuma(sqlStr, CommandType.Text);


                // burda gelen tipin nesne sini kuruyoz
                NesneDelagete = type.GetConstructor(Type.EmptyTypes);
                // delaget geliyo burda biz delagete kendi türüne cast ediyoz yani kendi türüne ceviriyoz
                Nesne = (T)NesneDelagete.Invoke(null);

                for (int i = 0; i < fieldSayısı - 1; i++)
                {
                    // sql den gelen proporti isinleri
                    var SqlProp = Okuyucu.GetName(i);
                    //sınıfın proportisi
                    var properti = type.GetProperty(SqlProp, BindingFlags.Instance | BindingFlags.Public);

                    if (Okuyucu.GetDataTypeName(i) == "int")
                    {
                        //sqlden gelen değer
                        var sqlPropDeğeri = Okuyucu.GetInt32(i);
                        //propetriye Sqlden cekilen degeri setliyoz
                        properti.SetValue(Nesne, sqlPropDeğeri);
                    }
                    if (Okuyucu.GetDataTypeName(i) == "nvarchar")
                    {
                        var sqlPropDeğeri = Okuyucu.GetString(i);
                        properti.SetValue(Nesne, sqlPropDeğeri);
                    }
                  //  burda bütün tipler için bunu yapıyoz

                }
                Okuyucu.Close();
            }
           
            return Nesne;
           
        }
        public T GetById( int kolondeğeri)
        {
            string kolonAdı="Id";
            foreach (var item in fieldNameGetAll())
            {
                if (item.ToString() == "ID")
                    kolonAdı = "ID";
                else if (item.ToString() == "ıd")
                    kolonAdı = "ıd";
                else if (item.ToString() == "İd")
                    kolonAdı = "id";
                else if (item.ToString() == "id")
                    kolonAdı = "id";
            }

           // string ModelAdı = type.Name;
            string sqlStr = $"select * from {ModelAdı} where {kolonAdı}={kolondeğeri}";
         var  Okuyucu = this.okuma(sqlStr, CommandType.Text);


            // burda gelen tipin nesne sini kuruyoz
            NesneDelagete = type.GetConstructor(Type.EmptyTypes);
            // delaget geliyo burda biz delagete kendi türüne cast ediyoz yani kendi türüne ceviriyoz
            Nesne = (T)NesneDelagete.Invoke(null);

            for (int i = 0; i < fieldSayısı - 1; i++)
            {
                // sql den gelen proportiisinleri
                var SqlProp = Okuyucu.GetName(i);
                //sınıfın proportisi
                var properti = type.GetProperty(SqlProp, BindingFlags.Instance | BindingFlags.Public);

                if (Okuyucu.GetDataTypeName(i) == "int")
                {
                    //sqlden gelen değer
                    var sqlPropDeğeri = Okuyucu.GetInt32(i);
                    //propetriye Sqlden cekilen degeri setliyoz
                    properti.SetValue(Nesne, sqlPropDeğeri);
                }
                if (Okuyucu.GetDataTypeName(i) == "nvarchar")
                {
                    var sqlPropDeğeri = Okuyucu.GetString(i);
                    properti.SetValue(Nesne, sqlPropDeğeri);
                }

            }
            Okuyucu.Close();
            return Nesne;
        }

        public List<T> sqlSorguGetir(string sqlStr)
        {

            string[] a=new string[20];
            string b="";
            int k = 0;
            for (int i = 0; i < sqlStr.Length; i++)
            {
                b += sqlStr[i].ToString();
                if(' ' == sqlStr[i])
                {
                    a[k]=b;
                    k++;
                }
            }
           int jk= a.Length;
            var Okuyucu = this.okuma(sqlStr, CommandType.Text);
            List<T> liste = new List<T>();

            while (Okuyucu.Read())
            {

                // burda gelen tipin nesne sini kuruyoz
                NesneDelagete = type.GetConstructor(Type.EmptyTypes);
                // delaget geliyo burda biz delagete kendi türüne cast ediyoz yani kendi türüne ceviriyoz
                Nesne = (T)NesneDelagete.Invoke(null);

                for (int i = 0; i < Okuyucu.FieldCount; i++)
                {
                    // sql den gelen proportiisinleri
                    var SqlProp = Okuyucu.GetName(i);
                    //sınıfın proportisi
                    var properti = type.GetProperty(SqlProp, BindingFlags.Instance | BindingFlags.Public);

                    if (Okuyucu.GetDataTypeName(i) == "int")
                    {
                        //sqlden gelen değer
                        var sqlPropDeğeri = Okuyucu.GetInt32(i);
                        //propetriye Sqlden cekilen degeri setliyoz
                        properti.SetValue(Nesne, sqlPropDeğeri);
                    }
                    if (Okuyucu.GetDataTypeName(i) == "nvarchar")
                    {
                        var sqlPropDeğeri = Okuyucu.GetString(i);
                        properti.SetValue(Nesne, sqlPropDeğeri);
                    }

                }
                liste.Add(Nesne);
            }
            return liste;
        }

        public void kaydet(T nesne)
        {
            string sqlStr = $"select * from {ModelAdı} ";
            var Okuyucu = this.okuma(sqlStr, CommandType.Text);

            string propertiler,değerler;
            propertiler = "";
            değerler = "";
            Okuyucu.Read();

           int y = type.GetMember("Properti").Count();
            for (int i = 1; i <fieldSayısı; i++)
            {
               

                var SqlProp = Okuyucu.GetName(i);

                var properti = type.GetProperty(SqlProp, BindingFlags.Instance | BindingFlags.Public);
                properti.GetValue(nesne);
             
                propertiler += $",{SqlProp}" ;
                // proportinin türünü alıyoz
                var tip= properti.PropertyType.Name;
                if (tip == "String")
                {
                    değerler +=$",'{properti.GetValue(nesne)}'" ;

                }
                else
                {
                    değerler +=","+ properti.GetValue(nesne);
                }

                
            }
            string propertilerim = "";
            string değerlerim="";
            for (int i = 1; i < değerler.Length; i++)
            {
                değerlerim += değerler[i];
            }
            for (int i = 1; i < propertiler.Length; i++)
            {
                propertilerim += propertiler[i];
            }
            Okuyucu.Close();
            string sql = $"insert into {ModelAdı}({propertilerim}) values({değerlerim})";
            kayıt(sql, CommandType.Text);
          
        }

        public void düzenle(T nesne)
        {
            string sqlStr = $"select * from {ModelAdı} ";
            var Okuyucu = this.okuma(sqlStr, CommandType.Text);

            string propertiler, değerler,Kolonlar, filter;
            propertiler = "";
            değerler = "";
            Kolonlar = "";
            filter = "";
            Okuyucu.Read();

        
            for (int i = 0; i < fieldSayısı; i++)
            {

             
                var SqlProp = Okuyucu.GetName(i);
     
                var properti = type.GetProperty(SqlProp, BindingFlags.Instance | BindingFlags.Public);
                properti.GetValue(nesne);

                propertiler = $"{SqlProp}";

                if (properti.Name == "id"|| properti.Name == "İd"|| properti.Name == "İD"|| properti.Name == "iD" || properti.Name == "ıd" || properti.Name == "Id" || properti.Name == "ID" || properti.Name == "ıD")
                {
                   
                    filter = $"{propertiler}={properti.GetValue(nesne)}";
                
                }else
                {
                   
                    // proportinin türünü alıyoz
                    var tip = properti.PropertyType.Name;
                    if (tip == "String")
                    {
                        değerler = $"'{properti.GetValue(nesne)}' ";

                    }
                    else
                    {
                        değerler =properti.GetValue(nesne).ToString();
                    }
                    Kolonlar += $", {propertiler}={değerler} ";
                }
              

            }
            string kolonlar = "";
            for (int i = 1; i < Kolonlar.Length; i++)
            {
                kolonlar += Kolonlar[i];
            }
            Okuyucu.Close();
          
            string sql = $"update {ModelAdı} set {kolonlar} where {filter}";
            kayıt(sql, CommandType.Text);
        }

        public void sil(T nesne)
        {
            string sqlStr = $"select * from {ModelAdı} ";
            var Okuyucu = this.okuma(sqlStr, CommandType.Text);


            string propertiler, filter;
            propertiler = "";
            filter = "";
            Okuyucu.Read();


            for (int i = 0; i < fieldSayısı; i++)
            {


                var SqlProp = Okuyucu.GetName(i);

                var properti = type.GetProperty(SqlProp, BindingFlags.Instance | BindingFlags.Public);
                properti.GetValue(nesne);

                propertiler = $"{SqlProp}";

                if (properti.Name == "id" || properti.Name == "İd" || properti.Name == "İD" || properti.Name == "iD" || properti.Name == "ıd" || properti.Name == "Id" || properti.Name == "ID" || properti.Name == "ıD")
                {

                    filter = $"{propertiler}={properti.GetValue(nesne)}";

                }
               


            }
            Okuyucu.Close();
            string sql = $"delete {ModelAdı}  where {filter}";
            kayıt(sql, CommandType.Text);
           
        }


        //public List<T> getir<T>() where T:class,new()
        ////{
        //// //var snf=  this.okuma(sqlStr, CommandType.Text);
        ////    var sn = this.tek(sqlStr, CommandType.Text); 
        ////    List<T> tolist = new List<T>();
        //// var vy =  snf.RecordsAffected;
        ////    T a = new T();
        ////    List<T> yt = new List<T>();
        ////    getbyId();
        ////    ArrayList ara = new ArrayList();
        ////    snf.Read();
        ////    for (int i = 0; i < snf.FieldCount; i++)
        ////    {

        ////    }

        //// return  yt.ToList();
        //    //if (T == snf.IsDBNull(1))
        //}
    }
}
