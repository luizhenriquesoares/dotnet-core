## API RESTful de usuários + login

### Stack

- dotnet core 2.2
- Redis
- Entity Framework Core

### Endpoints 

> Swagger Link:
[http://localhost:4000/swagger](http://localhost:4000/swagger)

![enter image description here](https://lh3.googleusercontent.com/-jYvrPSbtY2kQH3DQF3WsPfG3K8wfoin5tM2QZz3PNaHCQJRzJYDn7iW2uibj9wefrMuDpX7oHlM "teste")



  
```sh
localhost:4000/api/auth/signup
```

 Exemplo
 ```javascript
{
        "firstName": "Hello",
        "lastName": "World",
        "email": "hello@world2.com",
        "password": "hunter2",
        "phones": [
            {
                "number": 988887888,
                "area_code": 81,
                "country_code": "+55"
            }
        ]
    }
```


```sh
localhost:400/api/auth/signin
```

 Exemplo
 ```javascript
{
  "email": "hello@world2.com",
  "password": "hunter2"
}
```

##### Me

```sh
localhost:4000/api/me
```


 ```sh
Authorization: Bearer eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJoZWxsb0B3b3JsZDIuY29tIiwiYXV0aCI6eyJhdXRob3JpdHkiOiJ1c2VyIn0sImlhdCI6MTU0NjcyNTU2MywiZXhwIjoxNTQ2NzI5MTYzfQ.9i6Gyl5KSaDtIswkfCRpp5PEvJ53durimqfoeKxAMN0
```

##### Response

 ```javascript
{
    "firstName": "Hello",
    "lastName": "World",
    "email": "hello@world2.com",
    "phones": [
        {
            "number": 988887888,
            "area_code": 81,
            "country_code": "+55"
        }
    ],
    "created_at": "2019-07-02T18:59:23.05",
    "last_login": "2019-07-02T18:59:23.05"
}