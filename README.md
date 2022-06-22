# TwitchRerunFarm

Programa para farmar pontos em lives na twitch. você pode farmar em varias lives ao mesmo tempo, com baixo consumo de rede e de cpu, alem de poder usar vários usuários ao mesmo tempo.

## Configuração

Edite o arquivo Users.Json com seu Usuário e seu AuthToken, pode usar vários usuários. Para pegar o AuthToken de sua conta use esse website https://twitchapps.com/tmi/

```bash
[
	{

		"username": "ExampleUserName",
		"token": "ExampleAuthToken"
    
	},
    {

		"username": "ExampleUserName2",
		"token": "ExampleAuthToken3"
    
	}
]
```

## Canais

Edite o arquivo Channels.json com os canais que você quer farmar.

```{
  "Channels": [
    "gaules",
    "streamiebr",
	"baiano"
  ]
}
```
