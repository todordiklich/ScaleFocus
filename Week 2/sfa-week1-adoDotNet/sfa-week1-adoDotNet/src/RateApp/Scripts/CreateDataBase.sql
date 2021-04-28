CREATE TABLE [dbo].[RestaurantOwners](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[OwnerId] [int] NOT NULL,
 CONSTRAINT [PK_RestaurantOwners] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Restaurants]    Script Date: 4/28/2021 4:42:25 PM ******/

CREATE TABLE [dbo].[Restaurants](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Restaurants] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

/****** Object:  Table [dbo].[Reviews]    Script Date: 4/28/2021 4:42:25 PM ******/

CREATE TABLE [dbo].[Reviews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReviewText] [nvarchar](max) NOT NULL,
	[Rating] [int] NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[ReviewerId] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Reviews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

/****** Object:  Table [dbo].[Users]    Script Date: 4/28/2021 4:42:25 PM ******/

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Restaurants] ADD  CONSTRAINT [DF_Restaurants_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]

ALTER TABLE [dbo].[Reviews] ADD  CONSTRAINT [DF_Reviews_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]

ALTER TABLE [dbo].[RestaurantOwners]  WITH CHECK ADD  CONSTRAINT [FK_RestaurantOwners_Restaurants] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([Id])

ALTER TABLE [dbo].[RestaurantOwners] CHECK CONSTRAINT [FK_RestaurantOwners_Restaurants]

ALTER TABLE [dbo].[RestaurantOwners]  WITH CHECK ADD  CONSTRAINT [FK_RestaurantOwners_Users] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[Users] ([Id])

ALTER TABLE [dbo].[RestaurantOwners] CHECK CONSTRAINT [FK_RestaurantOwners_Users]

ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_RestaurantId_Restaurants_Id] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([Id])

ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_RestaurantId_Restaurants_Id]

ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_ReviewerId_Users_Id] FOREIGN KEY([ReviewerId])
REFERENCES [dbo].[Users] ([Id])

ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_ReviewerId_Users_Id]

