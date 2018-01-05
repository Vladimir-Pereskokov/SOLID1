using System;
using System.Collections.ObjectModel;
using SOLID.Store.DTOs;
using SOLID.Store.Entities.Repo;


namespace SOLID.Store.DataInfrastructure.MockData
{
    public static class MockProducts
    {
        //Books
        public const string MOCK_BOOK1_ID = "EEF7996D-2330-4057-8E25-277F0B22717B";
        public const string MOCK_BOOK1_NAME = "The Girl on the train";
        public const string MOCK_BOOK2_ID = "1BB05AFF-EEE9-4B2C-860F-792581522BE4";
        public const string MOCK_BOOK2_NAME = "Clean Code";
        public const string MOCK_BOOK3_ID = "0579B41D-2A87-4998-BD47-C26EA7D90F84";
        public const string MOCK_BOOK3_NAME = "Clean Architecture";
        //Videos
        public const string MOCK_VIDEO1_ID = "63F39D90-D555-421C-9D80-892E4943BE01";
        public const string MOCK_VIDEO1_NAME = "Comprehensive First Aid Training";
        public const string MOCK_VIDEO2_ID = "DDD58B50-18A8-4621-A064-97EC85DB7ECE";
        public const string MOCK_VIDEO2_NAME = "Modern Web Development with DDD";
        public const string MOCK_VIDEO3_ID = "49203DE1-5F43-4FB3-9D9D-31FCD0743778";
        public const string MOCK_VIDEO3_NAME = "Linux Cernel Overview";

        //Memebrship
        public const string MOCK_MEMBERSHIP_PRODUCT_ID = "4935B59D-48DF-4878-AA48-930B81CB2070";
        public const string MOCK_MEMBERSHIP_PRODUCT_Name = "Book Club Membership";

        private static ReadOnlyCollection<ProductDTO> m_Products;

        public static ReadOnlyCollection<ProductDTO> GetProducts()
        {
            if (m_Products == null)
            {
                m_Products = new ReadOnlyCollection<ProductDTO>(
                    new ProductDTO[] {
                        new ProductDTO(new Guid(MOCK_BOOK1_ID)) { Name = MOCK_BOOK1_NAME, Type = (int)ProductType.Book, FullPrice = 30M},
                        new ProductDTO(new Guid(MOCK_BOOK2_ID)) { Name = MOCK_BOOK2_NAME, Type = (int)ProductType.Book, FullPrice = 60M},
                        new ProductDTO(new Guid(MOCK_BOOK3_ID)) { Name = MOCK_BOOK3_NAME, Type = (int)ProductType.Book, FullPrice = 39.940M},
                        new ProductDTO(new Guid(MOCK_VIDEO1_ID)) { Name = MOCK_VIDEO1_NAME, Type = (int)ProductType.Video, FullPrice = 10M},
                        new ProductDTO(new Guid(MOCK_VIDEO2_ID)) { Name = MOCK_VIDEO2_NAME, Type = (int)ProductType.Video, FullPrice = 80M},
                        new ProductDTO(new Guid(MOCK_VIDEO3_ID)) { Name = MOCK_VIDEO3_NAME, Type = (int)ProductType.Video, FullPrice = 30M},
                        new ProductDTO(new Guid(MOCK_MEMBERSHIP_PRODUCT_ID))
                        { Name = MOCK_MEMBERSHIP_PRODUCT_Name, Type = (int)ProductType.Membership, FullPrice = 100M},
                    });
            }
            return m_Products;
        }
    }

}
