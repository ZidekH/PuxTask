<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProjectPux._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">



    <asp:TextBox ID="PathTextBox" runat="server" ></asp:TextBox>
    <asp:Button ID="StartButton" runat="server" Text ="Start" OnClick="GetChanges_Click" />

    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />

     <asp:GridView CssClass="table table-striped" ID="FilesGridView" runat="server" AllowPaging="false" 
            AllowSorting="False" emptydatatext="Žádné akce nebyly detekovány" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="CurrentFileState" HeaderText="Příznak" ReadOnly="True" />
                <asp:BoundField DataField="Name" HeaderText="Název souboru"  />
                <asp:BoundField DataField="Version" HeaderText="Verze" />
            </Columns>
        </asp:GridView>

    <br />
    <asp:Label ID="InfoLabel" runat="server"></asp:Label>
    



</asp:Content>
