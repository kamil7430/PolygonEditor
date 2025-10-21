using PolygonEditor.Presenter;

namespace PolygonEditor.View;

public partial class PolygonsForm : Form, IPolygonEditorView
{
    public PolygonsForm()
    {
        InitializeComponent();
        _ = new PolygonsFormPresenter(this);
    }

    public string KeyBindingsDescription
        => ">> Klawiszologia <<\n" +
        "- Przesuwanie wierzchołków: LPM\n" +
        "- Otwarcie menu kontekstowego wierzchołka lub krawędzi: PPM\n" +
        "- Przesunięcie całego wielokąta: Ctrl + LPM\n" +
        "\n" +
        ">> Opis algorytmu relacji (ograniczeń) <<\n" +
        "Każda krawędź może mieć tylko jedno ograniczenie (lub być łukiem/krzywą Beziera). " +
        "W momencie poruszenia wierzchołkiem, algorytm sprawdza, czy ograniczenie jest " +
        "spełnione, a jeśli nie to je zastosowuje i przechodzi do kolejnej krawędzi. " +
        "Powtarza tę sekwencję, aż w pewnej krawędzi w końcu będzie ono spełnione. " +
        "Zapamiętuje wierzchołek (def. A), na którym skończył i wykonuje sprawdzanie od poruszonego " +
        "wierzchołka, ale w drugą stronę. Jeżeli spotka krawędź, dla której sprawdzenie " +
        "zwróci spełnienie ograniczeń to kończy. Jeżeli natomiast dotrze do zapisanego wierzchołka A " +
        "to uznaje figurę za sztywną, przywraca poprzednie pozycje wierzchołków i przesuwa całość. " +
        "W przypadku poruszania punktem kontrolnym segmentu Beziera natomiast cały proces różni się " +
        "tylko początkową fazą, w której pierwsza krawędź jest rozpatrywana indywidualnie. Po jej " +
        "dostosowaniu uruchamiany jest algorytm wyjściowy.";
}
