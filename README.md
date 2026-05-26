# Snake Game MonoGame

Jogo da cobrinha feito em C# com MonoGame. O projeto usa sprites para a cobra, o rato e o placar, mantendo a estrutura simples e direta para estudo e evolução.

## Sobre o projeto

A ideia do jogo e simples: controlar a cobra com o teclado, coletar o rato, aumentar a pontuacao e evitar colisao com as bordas ou com o proprio corpo.

Controles:

- `W`: mover para cima
- `A`: mover para esquerda
- `S`: mover para baixo
- `D`: mover para direita
- `Esc`: sair do jogo

## Estrutura

- `Core`: classe principal do jogo e configuracoes gerais.
- `Entities`: entidades principais, como cobra e rato.
- `Systems`: entrada, colisao e pontuacao.
- `Graphics`: desenho dos sprites e animacao do rato.
- `UI`: elementos de interface, como o HUD.
- `Utils`: tipos e utilitarios simples.

## Assets

Os assets ficam na pasta `Content` e sao processados pelo MonoGame Content Builder usando o arquivo `Content.mgcb`.

O principal cuidado neste projeto foi integrar corretamente os assets com o codigo. A cobra usa partes diferentes do spritesheet para cabeca, corpo, cauda e curvas. O rato usa tres frames no spritesheet `rat_animat.png`, animados pela classe `RatAnimation`.

## Como executar

Restaure os pacotes e rode o projeto:

```bash
dotnet restore
dotnet run
```

Se houver problema com cache antigo de build, limpe `bin` e `obj` e rode novamente.
