using System;
using System.IO;

namespace SSDS_1
{

    public  enum Grade { Highest, First, Second };
    public enum Kind { Mutton, Veal, Pork, Chicken };
   
    public class Product
    {
        protected double[] gradePercent = new double[] { 0, 5, 7 };
        protected string sName;

        public string Name
        {
            get { return sName; }
            set { sName = value; }
        }

      
        protected double fPrice;

        public double Price
        {
            get { return fPrice; }
            set { fPrice = value; }
        }

        
        protected double fWeight;

        public double Weight
        {
            get { return fWeight; }
            set { fWeight = value; }
        }

        private int expTerm;

        public int ExpTerm
        {
            get { return expTerm; }
            set { expTerm = value; }
        }

        private DateTime manufactDate;

        public DateTime ManufactDay
        {
            get { return manufactDate; }
            set { manufactDate = value; }
        }

        


        public Product()
        {
            sName = "product";
            fPrice = 0;
            fPrice = 0;
        }
        public Product(string sName, double fPrice, double fWeight)
        {
            this.sName = sName;
            this.fPrice = fPrice;
            this.fWeight = fWeight;
        }
        public override string ToString()
        {
            return sName + " " + fPrice + "UAH  " + fWeight + "kg\n";
        }

        public override bool Equals(object obj)
        {
            if (obj==null||this.GetType() != obj.GetType())
                return false;
            Product p = (Product)obj;
            if (this.sName == p.sName)
                return true;
            else return false;
        }

        public virtual void PriceUp(double percentUp)
        {
            fPrice += fPrice * percentUp / 100;
        }

        public virtual void PriceDown(double percentUp)
        {
            fPrice -= fPrice * percentUp / 100;
        }
        public void Parse(string str)
        {
            //name price weight expTerm manufact Date
            string[] fields = str.Split(" ");
            sName = fields[0];
          
            try
            {
                fPrice = Convert.ToDouble(fields[1]);
                fWeight = Convert.ToDouble(fields[2]);
                expTerm = Convert.ToInt32(fields[3]);
               
            }
            catch (Exception)
            {

                throw;
            }
            try
            {
                
                DateTime d = DateTime.ParseExact (fields[4], "dd.MM.yyyy", null);

            }
            catch (Exception)
            {

                throw;
            }

            

        }

    }
   
    public class Meat : Product
    {
        
        private  Grade MeatGrade;

        public  Grade grade { 
            get { return MeatGrade; }
            set { MeatGrade = value; }
        }

        private Kind MeatKind;

        public Kind kind
        {
            get { return MeatKind; }
            set { MeatKind = value; }
        }
        public Meat():base()
        {
            MeatGrade = Grade.Highest;
            MeatKind = Kind.Pork;
        }
        public Meat(string Name, double Price, double Weight, Grade MeatGrade, Kind MeatKind):base(Name,Price,Weight)
        {
            this.MeatGrade = MeatGrade;
            this.MeatKind = MeatKind;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;
            Meat p = (Meat)obj;
            if (this.sName == p.sName&&this.fPrice==p.fPrice&&this.fWeight==p.fWeight&&this.MeatGrade==p.MeatGrade&&this.MeatKind==p.MeatKind) 
                return true;
            else return false;
        }
        public override string ToString()
        {
            return sName + " " + fPrice + "UAH  " + fWeight + "kg  " + MeatGrade + "  " + MeatKind + " \n";
        }
        public override void PriceUp(double percentUp)
        {
            switch (MeatGrade)
            {
                case Grade.Highest: 
                    { 
                        percentUp += gradePercent[2];
                        break;
                    }
                case Grade.First:
                    {
                        percentUp += gradePercent[1];
                        break;
                    }
                case Grade.Second:
                    {
                        percentUp += gradePercent[0];
                        break;
                    }
                default:break;
            }
            fPrice += fPrice * percentUp / 100;

        }

        public override void PriceDown(double percentUp)
        {
            fPrice -= fPrice * percentUp / 100;
        }
    }
    public class Dairy_products: Product
    {
        private int ExpTerm;
        public int expterm
        {
            get { return ExpTerm; }
            set { ExpTerm = value; }
        }
        public Dairy_products():base()
        {
            ExpTerm = 1;
        }
        public Dairy_products(string sName, double fPrice, double fWeight, int ExpTerm):base(sName, fPrice, fWeight)
        {
            this.ExpTerm = ExpTerm;
        }
        public override string ToString()
        {
            return sName + " " + fPrice + "UAH  " + fWeight + "kg  SHELF LIFE  " + ExpTerm + " days\n";
        }
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;
            Dairy_products p = (Dairy_products)obj;
            if (this.sName == p.sName && this.fPrice == p.fPrice && this.fWeight == p.fWeight && this.ExpTerm==p.ExpTerm)
                return true;
            else return false;
        }

        public override void PriceUp(double percentUp)
        {
            if (ExpTerm <= 10)
            {
                percentUp += gradePercent[0];
            }
           else if (ExpTerm >= 10||ExpTerm<=62)
            {
                percentUp += gradePercent[1];
            }
            if (ExpTerm>62)
            {
                percentUp += gradePercent[2];
            }

            fPrice += fPrice * percentUp / 100;

        }

        public override void PriceDown(double percentUp)
        {
            fPrice -= fPrice * percentUp / 100;
        }
    }

    public class Buy
    {
        private Product cProduct;
        public Product prod;

        public Product prdct
        {
            get { return prod; }
            set { prod = value; }
        }

        private int dAmount;

        public int Amount
        {
            get { return dAmount; }
            set { dAmount = value; }
        }

        public Buy()
        {
            dAmount = 1;
            cProduct = new Product();
        }
        public Buy(Product Prod, int dAmount)
        {
            this.dAmount = dAmount;
            cProduct = Prod;
        }
        public void PrintCheck()
        {
            Console.WriteLine($"{Amount} * {cProduct.ToString()} ");
        }
    }
    sealed public class Check:Buy
    {
        public Check(Product cProduct, int dAmount):base( cProduct, dAmount)
        {
                
        }
        
    }

    //public class My : Check
    //{

    //}


    public class Storage
    {
        protected Product[] products;
        int size;
        Grade GetGrade(int i)
        {
            Grade grad = Grade.Highest;
            switch (i)
            {
                case 0:
                    {
                        grad = Grade.Highest;
                        break;
                    }
                case 1:
                    {
                        grad = Grade.First;
                        break;
                    }
                case 2:
                    {
                        grad = Grade.Second;
                        break;
                    }
                default:
                    {
                        grad = Grade.Highest;
                        break;
                    }

            }
            return grad;
        }
        Kind GetKind(int i)
        {
            Kind kind = Kind.Mutton;
            switch (i)
            {
                case 0:
                    {
                        kind = Kind.Mutton;
                        break;
                    }
                case 1:
                    {
                        kind = Kind.Veal;
                        break;
                    }
                case 2:
                    {
                        kind = Kind.Pork;
                        break;
                    }
                case 3:
                    {
                        kind = Kind.Chicken;
                        break;
                    }
                default:
                    {
                        kind = Kind.Mutton;
                        break;
                    }

            }
            return kind;
        }
        public Storage()
        {
            Console.WriteLine("Enter amount of products: ");
            size = Convert.ToInt32(Console.ReadLine());
            products = new Product[size];

       
        }
        public void Create()
        {
           
            for(int i = 0; i < size; ++i)
            {
                Console.WriteLine("Meat(1), dairy(2) or other(3)?");
                int type = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter name of product: ");
                string nm = Console.ReadLine();
                Console.WriteLine("Enter price: ");
                double pr = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter weight: ");
                double wgh = Convert.ToDouble(Console.ReadLine());
                switch (type)
                {
                    case 1:
                            {
                            Console.WriteLine(" Enter grade: Highest(0), First(1), Second(2): ");
                            int grd = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine(" Enter kind: Mutton(0), Veal(1), Pork(2), Chicken(3): ");
                            int knd = Convert.ToInt32(Console.ReadLine());
                            products[i] = new Meat(nm, pr, wgh, GetGrade(grd), GetKind(knd));
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine(" Enter shell life(days) ");
                            int sl = Convert.ToInt32(Console.ReadLine());
                            products[i] = new Dairy_products(nm, pr, wgh, sl);
                            break;

                        }
                    default:
                        {
                            products[i] = new Product(nm, pr, wgh);
                            break;
                        }
                }
            }
        }

        public void Print_All()
        {
            for(int i = 0; i < size; ++i)
            {
                Console.WriteLine(Convert.ToString(products[i]));
            }
        }
        public void SearchMeat()
        {
            Console.WriteLine("MEAT:\n");
            for (int i = 0; i < size; ++i)
            {
                if(products[i] is Meat)
                {
                    Console.WriteLine(Convert.ToString(products[i]));
                }
            }
        }
        public void ChangePriceUp(double percent)
        {
            for (int i = 0; i < size; ++i)
            {
                products[i].PriceUp(percent);
            }
        }
        public void ChangePriceDown(double percent)
        {
            for (int i = 0; i < size; ++i)
            {
                products[i].PriceDown(percent);
            }
        }
        public Product this[int i]
        {
            get
            {
                return (Product)products[i];
            }


            set
            {
                products[i] = value;
            }
        }

        public void RemoveEllement(int ell)
        {
            size--;
            Product[] newarr = new Product[size];

            for(int i = 0; i < (size+1); ++i)
            {
                if (i != ell)
                {
                    newarr[i] = products[i];
                }
            }

            products = newarr;

        }
        public void WritetoFile(string filepath, int i)
        {
            try
            { 
                StreamWriter sw = new StreamWriter(filepath);
               
                sw.WriteLine(products[i].ToString());
                
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
           
        }

        public void RemoveSpoiled(string filepath)
        {
            DateTime now = DateTime.Now;
            for(int i = 0; i < size; ++i)
            {
                double n = (products[i].ManufactDay - now).TotalDays;
                if (n > products[i].ExpTerm)
                {
                    WritetoFile(filepath, i);
                    RemoveEllement(i);
                }
            }
        }

        public void ReadFromFile(string path)
        {
            
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                int i = 0;
                string line;
                try
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        products[i].Parse(line);
                    }
                }
                
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Storage a = new Storage();
            a.Create();
            Console.WriteLine("\nLIST OF PRODUCTS:\n");
            a.Print_All();
            a.SearchMeat();

        }
    }
}
