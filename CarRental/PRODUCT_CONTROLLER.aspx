<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PRODUCT_CONTROLLER.aspx.cs" Inherits="CarRental.ADD_CARS" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
      <br /> <br />
    <div class="pannel">
        <br />
         <asp:Button ID="Button2" runat="server" Text="ADD CAR OPTION" CssClass="butdesign" Font-Bold="True" Font-Size="Medium" OnClick="add_check" style="height: 40px; width:20%; border-radius: 0; " Visible ="False"/>
         <asp:Button ID="Button3" runat="server" Text="UPDATE/ DELETE CAR OPTION" CssClass="butdesign" Font-Bold="True" Font-Size="Medium" OnClick="delete_check" style="height: 40px;  border-radius: 0; width:43%" />        
    <br />
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Visible ="False">      
        </asp:DropDownList>
        <br />
        <br />
        <asp:Image runat ="server" ID ="image" ImageUrl="" style ="width:25%; height:25%;" Visible ="False"/>
        <br />
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
       <center><asp:FileUpload ID="prod_image" runat="server" CssClass="inputData" /></center>
        <br />
        <br />   
        <br />
        <asp:Button ID="Button1" runat="server" Text="ADD CAR" CssClass="butdesign" Font-Bold="True" Font-Size="Medium" OnClick="Add_Click" style="height: 40px"/>
        <asp:Button ID="Button5" runat="server" Text="UPDATE CAR" CssClass="butdesign" Font-Bold="True" Font-Size="Medium" OnClick="Update_Click" style="height: 40px" Visible ="False"/>
        <asp:Button ID="Button4" runat="server" Text="DELETE CAR" CssClass="butdesign" Font-Bold="True" Font-Size="Medium" OnClick="delete_click" style="height: 40px" Visible ="False"/>
        <br />
        <asp:Label ID="error" runat="server" CssClass="label" Font-Bold="False" Font-Size="Medium" Text =""></asp:Label>
        </div>

</asp:Content>
