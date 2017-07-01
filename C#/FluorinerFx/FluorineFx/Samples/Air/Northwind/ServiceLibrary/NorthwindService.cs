using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using FluorineFx;
using FluorineFx.AMF3;

namespace ServiceLibrary
{
    [RemotingService]
    public class NorthwindService
    {
        private string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString;
            return connectionString;
        }

        public List<EmployeeVO> GetEmployees()
        {
            List<EmployeeVO> result = new List<EmployeeVO>();
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
            {
                OleDbCommand command = new OleDbCommand("SELECT * FROM [Employees]", connection);
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    EmployeeVO employee = new EmployeeVO();
                    employee.id = reader.GetInt32(0);
                    employee.lastName= reader.GetString(1);
                    employee.firstName = reader.GetString(2);
                    employee.title = reader.IsDBNull(3) ? null : reader.GetString(3);
                    employee.titleOfCourtesy = reader.IsDBNull(4) ? null : reader.GetString(4);
                    if (!reader.IsDBNull(5))
                        employee.birthDate = reader.GetDateTime(5);
                    if (!reader.IsDBNull(6))
                        employee.hireDate = reader.GetDateTime(6);
                    employee.address = reader.IsDBNull(7) ? null : reader.GetString(7);
                    employee.city = reader.IsDBNull(8) ? null : reader.GetString(8);
                    employee.region = reader.IsDBNull(9) ? null : reader.GetString(9);
                    employee.postalCode = reader.IsDBNull(10) ? null : reader.GetString(10);
                    employee.country = reader.IsDBNull(11) ? null : reader.GetString(11);
                    employee.phone = reader.IsDBNull(12) ? null : reader.GetString(12);
                    employee.extension = reader.IsDBNull(13) ? null : reader.GetString(13);
                    employee.photo = reader.IsDBNull(14) ? null : reader.GetString(14);
                    employee.notes = reader.IsDBNull(15) ? null : reader.GetString(15);
                    if( !reader.IsDBNull(16) )
                        employee.reportsTo =  reader.GetInt32(16);
                    result.Add(employee);
                }
            }
            return result;
        }

        public ASObject GetEmployeePhoto(int employeeId)
        {
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
            {
                OleDbCommand command = new OleDbCommand(string.Format("SELECT [Image] FROM [Employees] WHERE [EmployeeID] = {0}", employeeId), connection);
                connection.Open();

                // 78 is the size of the OLE header for Northwind images.
                int offset = 78;
                byte[] data = (byte[])command.ExecuteScalar();
                MemoryStream ms = new MemoryStream();
                ms.Write(data, offset, data.Length - offset);
                Bitmap bmp24 = new Bitmap(ms);
                Bitmap bmp = new Bitmap(bmp24.Size.Width,bmp24.Size.Height,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bmp);
                g.DrawImage(bmp24, new Point(0, 0));
                g.Dispose();

                //Access bitmap data
                BitmapData lockData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);
                // Create an array to store image data
                int[] imageData = new int[bmp.Width * bmp.Height];
                // Use the Marshal class to copy image data
                System.Runtime.InteropServices.Marshal.Copy(lockData.Scan0, imageData, 0, imageData.Length);
                // Unlock image
                bmp.UnlockBits(lockData);
                //Write resulting image back in a ByteArray
                ByteArray ba = new ByteArray();
                for (int i = 0; i < imageData.Length; i++)
                {
                    ba.WriteUnsignedInt((uint)imageData[i]);
                }
                ba.Compress();
                ASObject result = new ASObject();
                result["bitmapData"] = ba;
                result["width"] = bmp.Width;
                result["height"] = bmp.Height;
                return result;
            }
        }
    }
}
