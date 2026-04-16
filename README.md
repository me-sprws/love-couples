# love-couples
A realtime quiz platform for couples to have fun, learn about each other, and strengthen their bond.

## System Design

### Functional requirements

- In another docs

### Unfunctional requirements

- 100_000 users
- QPS=1000
- API Latency = 200ms (p99<300ms)
- Realtime Latency = 50ms (p99<150ms)
- Realtime - stateful
- API - stateless

### Architecture

![Architecture](assets\images\system-desing.png)

### CouplesService API

#### FR

- Create, Invite and Manage Couples

#### UFR

- QPS = 1000
- Latency=100ms (p99<300ms)
- Stateless

#### Edge cases and QA

- What is a user? - *User - is a client who authenticated in a Couples Service and received a user_id.*
- What is a couples? - *Couples - is a two partners who agree to be together.*
- What is a partner (further **membership**)? - *Membership - is a user that agree to be together with an another partner (it's called **couples**).*
- How many memberships I can invite to a couples? - *I think a couples about one-one interaction and you can invite only one membership to your couples.*
- What happen if one of a memberships leave leaves the couples? - *We should archive couples and associate them with the ID of each membership.*
- How much couples you can have at the moment? - *We can't limit a number of your couples at the moment and you can have unlimited number*.

#### API

**Google OAuth**

```http
GET /google-sign-in HTTP/1.1
```

[AUTH] Update user info

```http
PUT /users/{id} HTTP/1.1
Content-Type: application/json

{
	"name": "Name",
	"birth_date": "Date",
	"country": "Country"
}
```

[AUTH] Create a new couples

```http
POST /couples HTTP/1.1
Content-Type: application/json
```

```http
HTTP/1.1 200 OK
Content-Type: application/json

{
	"couples_id": "Id",
	"together_since": "DateTime",
	"separatedAt": "DateTime",
	"createdAt": "DateTime",
	"status": "CouplesStatus",
	"members": 
	[
		{
			"membership_id": "Id",
			"user_id": "Id"
		}
	]
}
```

[AUTH] Generate an invitation code for a membership

Info: invitation code becomes invalid after accept.

```http
GET /couples/{id}/invite HTTP/1.1
```

```http
HTTP/1.1 200 OK
Content-Type: application/json

{
	"invitation_code": "Code"
}
```

[AUTH] Accept the Invitation code

Info: creates a pair of memberships (couples)

```http
POST /couples/invite/{code} HTTP/1.1
```

```http
HTTP/1.1 200 OK
Content-Type: application/json

{
	"couples_id": "Id",
	"together_since": "DateTime",
	"separatedAt": "DateTime",
	"createdAt": "DateTime",
	"status": "CouplesStatus",
	"members": 
	[
		{
			"membership_id": "Id",
			"user_id": "Id"
		}
	]
}
```

[AUTH] End the couples

Info: to end the couples, just update your couples status to "Separated".

#### Architecture

![DDD](C:\Users\ilyao\OneDrive\Desktop\Development\Backend\love-couples\assets\images\ddd-service-layer-dependencies.png)

**Source**: [Microsoft DDD-oriented microservice](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice).
