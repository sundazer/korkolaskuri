<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Korkolaskuri.aspx.cs" Inherits="Korkolaskuri.Korkolaskuri" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Korkolaskuri</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Korkolaskuri</h1>
    <div>
    Syötä tiedetyt arvot, jätä yksi kohta täyttämättä ja laskuri täyttää oikean arvon oikeaan kohtaan. Jos kaikki arvot on täytetty, laskee laskuri koron määrän.
        <br /><br />
    Alkupääoma: <asp:TextBox ID="txtAlkupaaoma" runat="server"></asp:TextBox> &nbsp€
        <br /><br />
    Korkoprosentti: <asp:TextBox ID="txtKorkoprosentti" runat="server"></asp:TextBox> &nbsp%
        <br /><br />
        <div>
    Korkoaika, valitse ensimmäisessä kalenterissa aloituspäivä ja seuraavassa kalenterissa lopetuspäivä:
        </div>
        <div style="float: left">
            <asp:Calendar ID="txtAikaAlku" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
            <NextPrevStyle VerticalAlign="Bottom" />
            <OtherMonthDayStyle ForeColor="#808080" />
            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
            <SelectorStyle BackColor="#CCCCCC" />
            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
            <WeekendDayStyle BackColor="#FFFFCC" />
        </asp:Calendar>
        </div>
        <div>
        <asp:Calendar ID="txtAikaLoppu"  runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
            <NextPrevStyle VerticalAlign="Bottom" />
            <OtherMonthDayStyle ForeColor="#808080" />
            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
            <SelectorStyle BackColor="#CCCCCC" />
            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
            <WeekendDayStyle BackColor="#FFFFCC" />
        </asp:Calendar>
        </div>
        <br /><br />
    Koron määrä: <asp:TextBox ID="txtKorkomaara" runat="server"></asp:TextBox> &nbsp€
    <br /><br />
        <asp:Button ID="btnLaske" runat="server" Text="Laske" OnClick="btnLaske_Click" />
        <asp:Label ID="lblInfo" runat="server" />
    </div>
    </form>
</body>
</html>
