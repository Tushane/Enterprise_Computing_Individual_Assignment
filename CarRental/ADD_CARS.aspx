<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ADD_CARS.aspx.cs" Inherits="CarRental.ADD_CARS" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   
     <br /> <br />
    <div class="pannel">
        
        <asp:Label ID="Label2" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text="Enter Car Name:"></asp:Label>
        <asp:TextBox ID="prod_name" runat="server" CssClass="inputData" ForeColor="#000001"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text="Enter Car Price:"></asp:Label>
        <asp:TextBox ID="prod_price" runat="server" CssClass="inputData" ForeColor="#000001"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text="Enter Car Description:"></asp:Label>
        <asp:TextBox ID="prod_desc" runat="server" CssClass="inputData" ForeColor="#000001" Height="79px" Width="209px" TextMode="MultiLine"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text="Enter Currency:"></asp:Label>
        <asp:TextBox ID="prod_cur" runat="server" CssClass="inputData" ForeColor="#000001" ReadOnly="True" Text="USD"></asp:TextBox>

         <br />
        <br />
        <asp:Label ID="Label5" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text="Select an Image:"></asp:Label>
        <asp:FileUpload ID="prod_image" runat="server" CssClass="inputData" />
        <br />
        <br />   
        <br />
        <asp:Button ID="Button1" runat="server" Text="ADD PRODUCT" CssClass="butdesign" Font-Bold="True" Font-Size="Medium" OnClick="Button1_Click" style="height: 36px"/>
        <br />
        <asp:Label ID="error" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text =""></asp:Label>
        </div>

</asp:Content>
