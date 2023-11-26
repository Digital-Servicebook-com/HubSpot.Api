using FluentAssertions;
using HubSpot.Api.Exceptions;
using HubSpot.Api.Models;
using System.Net;
using Xunit.Abstractions;

namespace HubSpot.Api.Test;

public class CompanyTests(ITestOutputHelper testOutputHelper) : TestBase(testOutputHelper)
{
	[Fact]
	public async void GetPageAsync_Succeeds()
	{
		var page = await Client.Crm.Companies.GetPageAsync();
		page.Results.Should().NotBeEmpty();
	}

	[Fact]
	public async void CreateReadUpdateAndDelete_Succeeds()
	{
		var createRequest = new CreateRequest
		{
			Properties = new Dictionary<string, object?>
			{
				{ "name", "Test Inc."},
				{ "website", "https://test.com/"},
			},
			Associations = []
		};

		HubSpotObject createdObject;
		try
		{
			createdObject = await Client.Crm.Companies.CreateAsync(createRequest);
			createdObject.Should().NotBeNull();
		}
		catch (HubSpotApiErrorException e) when (e.StatusCode == HttpStatusCode.Conflict)
		{
			e.Error.Category.Should().Be(ErrorCategory.Conflict);
			createdObject = new HubSpotObject
			{
				Id = e.Message.Split(' ').Last(),
				Properties = createRequest.Properties,
				Archived = false,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};
		}

		// Re-read the item
		var readObject = await Client.Crm.Companies.GetAsync(createdObject.Id);
		readObject.Should().NotBeNull();
		readObject.Id.Should().Be(createdObject.Id);
		readObject.Properties.Should().NotBeEmpty();

		// Delete the item
		await Client
			.Crm
			.Companies
			.ArchiveAsync(createdObject.Id);
	}
}