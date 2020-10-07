<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ErrorPage.aspx.cs" Inherits="CarRental.ErrorPage" %>

<asp:content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >
    <div id ="error_page">
        <h1>OOPS! THERE SEEM TO BE AN ISSUE</h1>
        <h2>Return to the Home Page by Clicking Below:</h2>
        <a runat="server" href="Default.aspx">CLICK HERE</a>
        <h3>HOWEVER IF THIS ISSUE PERSIST CONTACT THE ADMIN BY USING THE EMAIL BELOW</h3>
        <h3>admin@jakerental.com</h3>
    </div>
</asp:content>
