<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="apply.aspx.cs" Inherits="CarRental.apply" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="applyRunner" runat="server" Font-Bold="True" Font-Size="X-Large" ForeColor="White" Height="60%" Text="BECOME AN AFFORDABLE EXPLORER!" Width="100%" CssClass="teambanner" Font-Italic="True"></asp:Label>
    <br />
    <div class="pannel">
        
        <asp:Label ID="Label2" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text="First Name:"></asp:Label>
        <asp:TextBox ID="first_name" runat="server" CssClass="inputData" ForeColor="#000001"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text="Middle Name:"></asp:Label>
        <asp:TextBox ID="mid_name" runat="server" CssClass="inputData" ForeColor="#000001"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text="Last Name:"></asp:Label>
        <asp:TextBox ID="last_name" runat="server" CssClass="inputData" ForeColor="#000001"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text="Email Address:"></asp:Label>
        <asp:TextBox ID="email" runat="server" CssClass="inputData" ForeColor="#000001" TextMode="Email"></asp:TextBox>

         <br />
        <br />
        <asp:Label ID="Label5" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text="Phone Number:"></asp:Label>
        <asp:TextBox ID="phone_num" runat="server" CssClass="inputData" ForeColor="#000001" TextMode="Number"></asp:TextBox>
        <br />
        <br />   
        <asp:Label ID="Label9" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text="Username:"></asp:Label>
        <asp:TextBox ID="username" runat="server" CssClass="inputData" ForeColor="#000001"></asp:TextBox>
          <br />
        <br />
        <asp:Label ID="Label8" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text="Note: Your Password Length has to be 9 characters, Includes an Upper Case Letter."></asp:Label>
        <asp:Label ID="Label10" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text="Also, needs to have a Special Character which can be @, # or & and must include a numeric value."></asp:Label>
        <br />
        <asp:Label ID="Label6" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text="Create Password:"></asp:Label>
        <asp:TextBox ID="pass1" runat="server" CssClass="inputData" ForeColor="#000001" AutoPostBack="True"  MaxLength="9" TextMode="Password" OnTextChanged="pass1_TextChanged"></asp:TextBox>

        <br />
        <br />
        <asp:Label ID="Label7" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text ="Confirm Password:"></asp:Label>
        <asp:TextBox ID="pass2" runat="server" CssClass="inputData" ForeColor="#000001"  MaxLength="9" TextMode="Password"></asp:TextBox>
       <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="SIGN UP" CssClass="butdesign" Font-Bold="True" Font-Size="Medium" OnClick="Button1_Click" style="height: 36px"/>
        <asp:Button ID="Button2" runat="server" Text="CLEAR FORM" CssClass="butdesign" Font-Bold="True" Font-Size="Medium" OnClick="clear_form" style="height: 36px"/>       
        <br />
        <asp:Label ID="error" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text =""></asp:Label>
        </div>
</asp:Content>
