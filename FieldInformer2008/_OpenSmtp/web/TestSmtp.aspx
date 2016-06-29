<%@ Import Namespace="OpenSmtp.Mail" %>
<%@ Page Trace="true" TraceMode="SortByCategory" %>
<html>
<style>

	div 
	{ 
		font: 8pt verdana;
		background-color:cccccc;
		border-color:black;
		border-width:1;
		border-style:solid;
		padding:10,10,10,10; 
	}

</style>
<script language="C#" runat="server">

  public void SubmitBtn_Click(Object sender, EventArgs E) 
  {

	try
	{
		string 	smtpHost 			= "localhost";
		int 	smtpPort 			= 25;
		string	tempDir				= "C:\\cvs\\openSmtp\\web\\temp";
		string 	senderEmail 		= Request.Form.Get("from");	
		string 	recipientEmail		= Request.Form.Get("to");
		string 	subject 			= Request.Form.Get("subject");
		string 	body 				= Request.Form.Get("body");


		Message.InnerHtml = null;
		Message.Style["visibility"]= "show";

		SmtpConfig.VerifyAddresses = false;
		
		MailMessage msg = new MailMessage(senderEmail, recipientEmail);

		msg.Subject = subject;
		msg.Body = body;

		Smtp smtp = new Smtp(smtpHost, smtpPort);
		smtp.SendMail(msg);
		
		
		StringBuilder toList = new StringBuilder();
		for (IEnumerator i = msg.To.GetEnumerator(); i.MoveNext();)
		{
			EmailAddress a = (EmailAddress)i.Current;
			toList.Append(a.Address + ";");
		}	

		Message.InnerHtml += "<b>SendMail Results:</b><br>";
		Message.InnerHtml += "To:" + toList + "<br>";
		Message.InnerHtml += "From:" + msg.From.Address + "<br>";
		Message.InnerHtml += "Subject:" + msg.Subject + "<br>";
		Message.InnerHtml += "Body:" + msg.Body + "<br>";

	}
	catch(MalformedAddressException mfa)
	{
		Message.InnerHtml += "Address error occured: " + mfa.Message;
	}
	catch(SmtpException se)
	{
		Message.InnerHtml += "Smtp error occured: " + se.Message;
	}
	catch(Exception e)
	{
		Message.InnerHtml += "Error occured: " + e.Message + "r\n" + e;
	}

  }
  

</script>

<body style="font: 10pt verdana">
<b><h3>Basic Smtp Example</h3></b><br><br>

<form action="form.aspx" enctype="multipart/form-data" method="post" runat="server">
	<P>
		<TABLE height="27" cellSpacing="1" cellPadding="1" width="387" border="0">
			<TR>
				<TD style="WIDTH: 95px">
					<FONT face="Verdana" size="2">From:</FONT>
				</TD>
				<TD>
					<INPUT type="text" name="from" size="70"/>
				</TD>
			</TR>
			<TR>
				<TD style="WIDTH: 95px">
					<FONT face="Verdana" size="2">To:</FONT>
				</TD>
				<TD>
					<INPUT type="text" name="to" size="70"/>
				</TD>
			</TR>
			<TR>
				<TD style="WIDTH: 95px">
					<FONT face="Verdana" size="2">Cc:</FONT>
				</TD>
				<TD>
					<INPUT type="text" name="cc" size="70"/>
				</TD>
			</TR>
			<TR>
				<TD style="WIDTH: 95px">
					<FONT face="Verdana" size="2">Subject:</FONT>
				</TD>
				<TD>
					<INPUT type="text" name="subject" size="70"/>
				</TD>
			</TR>
			

			<TR>
				<TD style="WIDTH: 95px" valign="top">
					<FONT face="Verdana" size="2">Body:</FONT>
				</TD>
				<TD>
					<TEXTAREA cols="60" rows="10" name="body"></TEXTAREA>
				</TD>
			</TR>
			<TR>
				<TD style="WIDTH: 95px">
					&nbsp;
				</TD>
				<TD>
					<asp:button name="Submit" text="Send Mail" OnClick="SubmitBtn_Click" runat="server"/>
				</TD>
			</TR>
		</TABLE>
		<br>
		<br>		
		<div id="Message" style="visibility:hidden;" runat="server"/>
		
		
		</FONT>
		
	</P>
</form>

</body>
</html>