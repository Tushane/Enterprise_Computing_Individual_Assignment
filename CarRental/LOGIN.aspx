<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="LOGIN.aspx.cs" Inherits="CarRental.LOGIN" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 <div class ="main_pannel">
     <center>
    <div class ="pannel" id ="log_pannel">

        <br />
            <asp:TextBox ID="username" placeholder="Enter Your Email"  runat="server" CssClass="log_inputData" ForeColor="#000001" Wrap="False"></asp:TextBox>
           <br />
            <br />
            <asp:TextBox ID="user_password" placeholder="Enter Your Password" runat="server" CssClass="log_inputData" TextMode="Password" ForeColor="#000001"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="LOGIN" CssClass="butdesign" Font-Bold="True" Font-Size="Medium"/>
        <hr/>    
        <p>Forget Your Password Click <a runat="server" href="" style="color:blue">Here</a></p>
    </div>
       </center>
  </div>
</asp:Content>

