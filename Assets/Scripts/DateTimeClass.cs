using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class DateTimeClass
{
    public static DateTime GetDummyDate()
    {
        return new DateTime(1000, 1, 1); //to check if we have an online date or not.
    }

    public static DateTime GetNISTDate()
    {
        Random ran = new Random(DateTime.Now.Millisecond);
        DateTime date = GetDummyDate();
        string serverResponse = string.Empty;
        //        string[] servers = new string[]
        //        {
        //        "0.pool.ntp.org",
        //"1.pool.ntp.org",
        //"2.pool.ntp.org",
        //"3.pool.ntp.org"
        //    };

       // Represents the list of NIST servers
        string[] servers = new string[]
        {
        "nist1-la.ustiming.org",
        "nist1-ny.ustiming.org",
        "time-a.nist.gov",
        "nist1-chi.ustiming.org",
        "time.nist.gov",
        "ntp-nist.ldsbc.edu"
    };

        // Try each server in random order to avoid blocked requests due to too frequent request
        for (int i = 0; i < servers.Length; i++)
        {
            try
            {
                
                // Open a StreamReader to a random time server
                //StreamReader reader = new StreamReader(new System.Net.Sockets.TcpClient(servers[ran.Next(0, servers.Length)], 13).GetStream());
                StreamReader reader = new StreamReader(new System.Net.Sockets.TcpClient(servers[i], 13).GetStream());
                serverResponse = reader.ReadToEnd();
                reader.Close();

                // Check to see that the signature is there
                if (serverResponse.Length > 47 && serverResponse.Substring(38, 9).Equals("UTC(NIST)"))
                {
                    // Parse the date
                    int jd = int.Parse(serverResponse.Substring(1, 5));
                    int yr = int.Parse(serverResponse.Substring(7, 2));
                    int mo = int.Parse(serverResponse.Substring(10, 2));
                    int dy = int.Parse(serverResponse.Substring(13, 2));
                    int hr = int.Parse(serverResponse.Substring(16, 2));
                    int mm = int.Parse(serverResponse.Substring(19, 2));
                    int sc = int.Parse(serverResponse.Substring(22, 2));

                    if (jd > 51544)
                        yr += 2000;
                    else
                        yr += 1999;

                    date = new DateTime(yr, mo, dy, hr, mm, sc);

                    // Exit the loop
                    break;
                }
            }
            catch (Exception ex)
            {
                /* Do Nothing...try the next server */
            }
        }
        return date;
    }
}

