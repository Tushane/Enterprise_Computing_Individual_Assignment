<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CART_HIST.aspx.cs"  MasterPageFile="~/Site.Master"  Inherits="CarRental.CART_HIST" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div ID ="temp_cart_data" runat ="server">

      <h1>YOUR HAVE NO ORDER COMPLETED!</H1>

    </div>


    <asp:PlaceHolder ID ="main" runat ="server">

    </asp:PlaceHolder>

</asp:Content>