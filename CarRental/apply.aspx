<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="apply.aspx.cs" Inherits="CarRental.apply" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="applyRunner" runat="server" Font-Bold="True" Font-Size="X-Large" ForeColor="White" Height="60%" Text="JOIN OUR ELITE SQUAD  OF EXPLORERS" Width="100%" CssClass="teambanner" Font-Italic="True"></asp:Label>
    <br />
    <div class="pannel">
        
        <asp:Label ID="Label2" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text="First Name:"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" CssClass="inputData" ForeColor="#000001"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text="Last Name:"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server" CssClass="inputData" ForeColor="#000001"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text="Email Address:"></asp:Label>
        <asp:TextBox ID="TextBox3" runat="server" CssClass="inputData" ForeColor="#000001"></asp:TextBox>

         <br />
        <br />
        <asp:Label ID="Label5" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text="Phone Number:"></asp:Label>
        <asp:TextBox ID="TextBox4" runat="server" CssClass="inputData" ForeColor="#000001"></asp:TextBox>

          <br />
        <br />
        <asp:Label ID="Label6" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text="Create Password:"></asp:Label>
        <asp:TextBox ID="TextBox5" runat="server" CssClass="inputData" ForeColor="#000001" MaxLength="9"></asp:TextBox>

        <br />
        <br />
        <asp:Label ID="Label7" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text="Confirm Password:"></asp:Label>
        <asp:TextBox ID="TextBox6" runat="server" CssClass="inputData" ForeColor="#000001" MaxLength="9"></asp:TextBox>
       <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="SIGN UP" CssClass="butdesign" Font-Bold="True" Font-Size="Medium"/>
        <asp:Button ID="Button2" runat="server" Text="CLEAR FORM" CssClass="butdesign" Font-Bold="True" Font-Size="Medium" />
    </div>
</asp:Content>
