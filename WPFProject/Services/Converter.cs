using System.Xml.Xsl;
using System.Xml;
using System.Xml.XPath;

namespace WPFProject.Services
{
    /// <summary>
    /// Представляет класс запускающий конвертацию xml посредством xslt.
    /// </summary>
    internal class Converter
    {
        #region Поля

        private readonly XslTransform _xslTransform;
        private XmlTextWriter? _xmlTextWriter;
        private XPathDocument? _xPathDocument;

        #endregion Поля

        #region Конструктор

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Converter"/>.
        /// </summary>
        public Converter()
        {
            _xslTransform = new XslTransform();
        }

        #endregion Конструктор

        #region Методы

        /// <summary>
        /// Конвертирует xml посредством xslt-преобразования.
        /// </summary>
        /// <param name="inputPath"> Путь xml файла для преобразования. </param>
        /// <param name="outputPath"> Путь для нового xml файла. </param>
        public void Convert(string inputPath, string outputPath)
        {
            _xmlTextWriter = new XmlTextWriter(outputPath, null);
            _xPathDocument = new XPathDocument(inputPath);

            _xslTransform.Load("Services/XslTransform.xslt");
            _xslTransform.Transform(_xPathDocument, null, _xmlTextWriter);
            _xmlTextWriter.Close();
        }

        #endregion Методы

    }
}
