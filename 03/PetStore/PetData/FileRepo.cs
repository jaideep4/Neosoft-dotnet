using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Text.Json;
using System;
using System.Threading.Tasks;
namespace PetData
{
    public class FileRepo:IRepo
    {
        static List<Cat> cats=null;
        static string path= @"..\Cats.xml";
        public List<Cat> Init(){
                cats=new List<Cat>(){
                    new Cat(){Id=100, CatType=CatType.Abyssian, Dob=new System.DateTime(2013,12,13),Gender=Gender.Female,Name="Kitty", Weight=7.5, LegLength=2, RibCage=14},
                    new Cat(){Id=101, CatType=CatType.Balinese_Javanese, Dob=new System.DateTime(2016,02,23),Gender=Gender.Male,Name="Billy", Weight=8.5,LegLength=2.2, RibCage=15},
                    new Cat(){Id=102, CatType=CatType.Bengal, Dob=new System.DateTime(2021,12,13),Gender=Gender.Female,Name="Snow", Weight=4.5,LegLength=1.8, RibCage=14}
                };
           return cats;
        }
        public void AddDummyCats(List<Cat> cats){
            StreamWriter writer=null;
            try{
                writer = new StreamWriter(path);
                XmlSerializer serializer =new XmlSerializer(typeof(List<Cat>));
                serializer.Serialize(writer,cats); 
            }
            catch(DirectoryNotFoundException ex){
                System.Console.WriteLine(ex.Message);
            }
            catch(FileNotFoundException ex){
                System.Console.WriteLine(ex.Message);
            }
            catch(Exception ex){
                System.Console.WriteLine(ex.Message);
            }                
            finally{              
                writer.Close();
            }
            System.Console.WriteLine("All cats has bee stored in the XML file at {0}",path);
        }

        public async void AddDummyCats_Json(IEnumerable<Cat> cats,string _path = @".\Cats.json"){
           using(FileStream stream =File.Create(_path)){
               try{
                    await JsonSerializer.SerializeAsync(stream, cats);
               }
               catch(DriveNotFoundException ex){
                   Console.WriteLine(ex.Message);
               }           
               catch(FileNotFoundException ex){
                   Console.WriteLine(ex.Message);
               }
               System.Console.WriteLine("Cats are added to {0} \n",_path);
           }           
        }
        public Stack<Cat> Init_Stack(){
            Stack<Cat> cats=new Stack<Cat>();
            cats.Push( new Cat(){Id=100, CatType=CatType.Abyssian, Dob=new System.DateTime(2013,12,13),Gender=Gender.Female,Name="Kitty", Weight=7.5});
            cats.Push( new Cat(){Id=101, CatType=CatType.Balinese_Javanese, Dob=new System.DateTime(2016,02,23),Gender=Gender.Male,Name="Billy", Weight=8.5});
            cats.Push(new Cat(){Id=102, CatType=CatType.Bengal, Dob=new System.DateTime(2021,12,13),Gender=Gender.Female,Name="Snow", Weight=4.5});
            return cats;
        }

        public Dictionary<int, Cat> Init_Dictionary(){
            Dictionary<int,Cat> cats = new Dictionary<int, Cat>();
            cats.Add(1, new Cat(){Id=102, CatType=CatType.Bengal, Dob=new System.DateTime(2021,12,13),Gender=Gender.Female,Name="Snow", Weight=4.5});
            cats.Add(2,new Cat(){Id=101, CatType=CatType.Balinese_Javanese, Dob=new System.DateTime(2016,02,23),Gender=Gender.Male,Name="Billy", Weight=8.5});
            cats.Add(3,new Cat(){Id=100, CatType=CatType.Abyssian, Dob=new System.DateTime(2013,12,13),Gender=Gender.Female,Name="Kitty", Weight=7.5});
            return cats;
        }
        public void Add(Cat cat){
            cats.Add(cat);
            System.Console.WriteLine("Cat has been added");
        }

    public IEnumerable<Cat> GetAllCats(string _path= @"..\Cats.xml"){
            XmlSerializer deserializer=null;
            List<Cat> cats=null;
            try{
                using StreamReader reader=new StreamReader(_path);
                deserializer=new XmlSerializer(typeof(List<Cat>));
                var result = (List<Cat>)deserializer.Deserialize(reader);
               }
                catch(DirectoryNotFoundException ex){
                    System.Console.WriteLine("Invalid path to the file");
                }
                catch(FileNotFoundException ex){
                    System.Console.WriteLine("Invalid path to the file");
                }
                catch(Exception ex){
                    Console.WriteLine("Exception");
                }  
                if(cats!=null){
                    if(cats.Count>0)
                        return cats;
                }
                else
                    throw new System.NullReferenceException();
                    return null;
   }
      
        public Cat GetCat(int id){
            var cats= GetAllCats();
            var cat=cats.Where<Cat>(x=>x.Id==id).FirstOrDefault();
            return cat;
        }
        public Cat GetCat(string name){
            var cats= GetAllCats();
            var cat=cats.Where<Cat>(x=>x.Name==name).FirstOrDefault();
            return cat;
        }
    }
}