using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace WPFProject.Services
{
    /// <summary>
    /// Представляет базовый класс по работе с xml документами.
    /// </summary>
    internal class XmlTransformer
    {
        #region Методы

        /// <summary>
        /// Возвращает главную ноду.
        /// </summary>
        /// <param name="xDoc"> Xml документ. </param>
        /// <returns> Главный родитеский элемент. </returns>
        protected XmlNode? GetMainNode(XmlDocument xDoc) => xDoc.DocumentElement;

        /// <summary>
        /// Находит все ноды предков, содержащие ноды <paramref name="elementsForSearch"/>.
        /// </summary>
        /// <param name="nodesForSearch"> Ноды по которым ведется поиск. </param>
        /// <param name="foundNodes"> Найденные ноды. </param>
        /// <param name="elementsForSearch"> Имя элементов для поиска. </param>
        /// <param name="counter"> Счетчик для предотвращения ухода в бесконечный цикл. </param>
        private void GetElementsParents(List<XmlNode?> nodesForSearch, List<XmlNode?> foundNodes, string elementsForSearch, int counter = 0)
        {
            if (counter < 3)
            {
                foreach (var node in nodesForSearch)
                {
                    var childrens = node?.SelectNodes(elementsForSearch);

                    if (childrens?.Count > 0)
                    {
                        foundNodes.Add(node);
                    }
                    else if (node?.ChildNodes?.Count > 0)
                    {
                        List<XmlNode?> items = node.ChildNodes.Cast<XmlNode?>().ToList();
                        GetElementsParents(items, foundNodes, elementsForSearch, counter++);
                    }
                }
            }
        }

        protected List<XmlNode?> GetAllNodesForChildCount(XmlDocument doc)
        {
            List<XmlNode?> nodesForSearch = new List<XmlNode?>() { GetMainNode(doc) };

            List<XmlNode?> foundNodes = new List<XmlNode?>();

            GetElementsParents(nodesForSearch, foundNodes, "item");

            return foundNodes;
        }

        #endregion Методы

    }
}
