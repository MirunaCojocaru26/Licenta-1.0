﻿using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendEmail : MonoBehaviour
{
    public InputField recipentEmail;
    public InputField bodyMessage;

    public void Send()
    {
        // Create mail
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("mirunacojocaru26@yahoo.com");
        mail.To.Add("mirunacojocaru26@yahoo.com");
        mail.Subject = recipentEmail.text;
        mail.Body = bodyMessage.text;

        // Setup server 
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new NetworkCredential("mirunacojocaru26@yahoo.com", "MyPassword*") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors) {
                Debug.Log("Email success!");
                return true;
            };

        // Send mail to server, print results
        try
        {
            smtpServer.Send(mail);
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            SSTools.ShowMessage("Messge send", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        catch (System.Exception e)
        {
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            SSTools.ShowMessage("Error on send", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
    }
}

