# MagicEasyDeckBuilder

Projeto para um **Deck Builder** para o cardgame *Magic the Gathering*, com cadastro de usuarios e gerenciamento de multiplos decks, em variados formatos de jogo.

O projeto está sendo implementado utilizando **ASP.NET Core** no back-end, utilizando o ORM **Entity Framework*, na modalidade Code First, com banco de dados *PostgreSQL*, no fornt-end está sendo utilizado o Angular 14, junto do Bootstrap e Sass.

O projeto está sendo desenvolvido utilizando *TDD (Test Dirven Design)*, com testes em *xUnit* principalmente para garantia das regras de negocios envolvendo a validação dos Decks.

A arqitetura de camadas, com a utilização de Dominios Ricos, servem para aproximar o projeto do uso de *DDD (Domain Driven Design)*.

A ideia de layout do Site encontrasse no [Figma](https://www.figma.com/file/8nArsNwtQPK7fLEhhpJq8h/Site?t=Ia6ZR1Sc7yHn7Jhy-6).

As informações em relações as Cartas são obtidas utilizando a [API do Skryfall](https://scryfall.com/docs/api), um site para busca de cartas de MTG.

O projeto ainda encontrasse em desenvolvimento, mas tem boa parte da API completa e o cadastro e login já implementado no Fornt.

