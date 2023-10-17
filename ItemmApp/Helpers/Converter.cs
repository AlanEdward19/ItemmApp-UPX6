using Xceed.Words.NET;

namespace ItemmApp.Helpers
{
    public static class Converter
    {
        public static void FillDOCX(string docxTemplatePath, string docxOutputPath, Dictionary<string, string> changes)
        {
            using (var doc = DocX.Load(docxTemplatePath))
            {
                foreach (var change in changes)
                {
                    string searchText = change.Key;
                    string newValue = change.Value;

                    // Leitura do DOCX para encontrar a posição do texto
                    foreach (var paragraph in doc.Paragraphs)
                    {
                        if (paragraph.Text.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                        {
                            // Encontrou o texto no parágrafo, substitui com o novo valor
                            paragraph.ReplaceText(searchText, newValue, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        }
                    }
                }

                // Salva o documento modificado
                doc.SaveAs(docxOutputPath);
            }
        }

        public static string FormatCpf(string cpf) => Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
    }
}
