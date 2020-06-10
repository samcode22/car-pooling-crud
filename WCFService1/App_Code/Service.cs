using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    public Service()
    {
        ConnectToDb();
    }

    SqlConnection conn;
    SqlCommand comm;

    SqlConnectionStringBuilder connStringBuilder;

    void ConnectToDb()
    {
        /* connStringBuilder = new SqlConnectionStringBuilder();
         connStringBuilder.DataSource = "(LocalDB)";
         connStringBuilder.InitialCatalog = "WCF";
         connStringBuilder.Encrypt = true;
         connStringBuilder.TrustServerCertificate = true;
         connStringBuilder.ConnectTimeout = 30;
         connStringBuilder.AsynchronousProcessing = true;
         connStringBuilder.MultipleActiveResultSets = true;
         connStringBuilder.IntegratedSecurity = true;

         conn = new SqlConnection(connStringBuilder.ToString());*/
       conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);

        comm = conn.CreateCommand();

    }

	public string GetData(int value)
	{
		return string.Format("You entered: {0}", value);
	}

	public CompositeType GetDataUsingDataContract(CompositeType composite)
	{
		if (composite == null)
		{
			throw new ArgumentNullException("composite");
		}
		if (composite.BoolValue)
		{
			composite.StringValue += "Suffix";
		}
		return composite;
	}

    public int InsertCar(Car c)
    {
        try
        {
            comm.CommandText = "INSERT INTO Car VALUES(@Id , @Car_Type , @Car_Name , @Driver , @Car_Number)";
            comm.Parameters.AddWithValue("Id", c.Id);
            comm.Parameters.AddWithValue("Car_Type", c.Car_Type);
            comm.Parameters.AddWithValue("Car_Name", c.Car_Name);
            comm.Parameters.AddWithValue("Driver", c.Driver);
            comm.Parameters.AddWithValue("Car_Number", c.Car_Number);

            comm.CommandType = CommandType.Text;
            conn.Open();


            return comm.ExecuteNonQuery();
        }
        catch(Exception)
        {
            return -1;
        }
        finally
        {
            
            if(conn != null)
            {
                conn.Close();
            }
        }
    }


    public int UpdateCar(Car c)
    {
        try
        {
            comm.CommandText = "UPDATE Car SET Car_Type = @Car_Type , Car_Name = @Car_Name , Driver = @Driver , Car_Number = @Car_Number WHERE Id = @Id ";
            comm.Parameters.AddWithValue("Id", c.Id);
            comm.Parameters.AddWithValue("Car_Type", c.Car_Type);
            comm.Parameters.AddWithValue("Car_Name", c.Car_Name);
            comm.Parameters.AddWithValue("Driver", c.Driver);
            comm.Parameters.AddWithValue("Car_Number", c.Car_Number);

            comm.CommandType = CommandType.Text;
            conn.Open();

            return comm.ExecuteNonQuery();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if (conn != null)
            {
                conn.Close();
            }
        }
    }


    public int DeleteCar(Car c)
    {
        try
        {
            comm.CommandText = "DELETE Car WHERE Id = @Id";
            comm.Parameters.AddWithValue("Id", c.Id);
          
            comm.CommandType = CommandType.Text;
            conn.Open();

            return comm.ExecuteNonQuery();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if (conn != null)
            {
                conn.Close();
            }
        }
    }


    public Car GetCar(Car c)
    {
        Car car = new Car();
        try
        {
            comm.CommandText = "select * from Car WHERE Id = @Id";
            comm.Parameters.AddWithValue("Id", c.Id);

            comm.CommandType = CommandType.Text;
            conn.Open();

            SqlDataReader reader = comm.ExecuteReader();
            while(reader.Read())
            {
                car.Id = Convert.ToInt32(reader[0]);
                car.Car_Type = reader[1].ToString();
                car.Car_Name = reader[2].ToString();
                car.Driver = reader[3].ToString();
                car.Car_Number = reader[4].ToString();

            }
            return car;

        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if (conn != null)
            {
                conn.Close();
            }
        }
    }

    


    public List<Car> GetAllCars()
    {
        List<Car> carD = new List<Car>();
        try
        {
            comm.CommandText = "select * from Car";
            comm.CommandType = CommandType.Text;
            conn.Open();

            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Car car = new Car()
                {
                    Id = Convert.ToInt32(reader[0]),
                    Car_Type = reader[1].ToString(),
                    Car_Name = reader[2].ToString(),
                    Driver = reader[3].ToString(),
                    Car_Number = reader[4].ToString()
                };
                carD.Add(car);

            }
            return carD;

        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if (conn != null)
            {
                conn.Close();
            }
        }
    }
}
