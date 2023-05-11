# desafio-tec-churras

Essa aplicacão Web foi desenvolvida em .NET 7 e 
é basicamente um CRUD criado com a utilização de Razor Pages.

A aplicação utiliza o Identity para Login e Cadastro de
novos clientes, como também a autorização de acesso em
algumas telas da aplicação.

[Login (img)](https://i.imgur.com/IjjPp8H.png) | [Cadastro (img)](https://i.imgur.com/RfxU9my.png)

Após o cadastro, o usuário será redirecionado para a tela inicial (index)
onde poderá cadastrar/marcar um Churras,
que terá os seguintes campos para serem preenchidos:

[Index (img)](https://i.imgur.com/D37F2KI.png)

- Nome*
- Valor de contribuição de churras (carne)*
- Valor de contribuição de bebidas*
- Data*
- Estimativa de pessoas*
- Descrição*
- Informações adicionais

[Cadastrar/Marcar Churras (img)](https://i.imgur.com/OjRIzz2.png)

Ao cadastrar/marcar um Churras,
o usuário será redireciona para a tela inicial (Index),
que listara um card com as informações do churras,
tais como: Data, Nome, Descrição, Participantes e Valor total arrecadado.
Além das opções de editar, ver os detalhes e remover/desmarcar o Churras.

[Cards (img)](https://i.imgur.com/35BbpNw.png)

Ao clicar na edição, por exemplo, além de ter a opção
de editar os dados do Churras, o usuário poderá convidar os participantes 
do Churras. Ao clicar em convidar, o usuário será levado
para a tela de cadastro de novo participante, que terá
os seguintes campos a serem preenchidos:

- Nome*
- Valor de contribuição em churras (carne)*
- Valor de contribuição em bebidas*
- Participante confirmado* (caixa de seleção)
- Churrasco* (select com o Churras em questão já selecionado)

[Editar Churras (img)](https://i.imgur.com/orsiHCP.png) | [Convidar participantes (img)](https://i.imgur.com/BSIU3W2.png)

Após o cadastro deste participante, o usuário será
redirecionado novamente para a tela de edição
do Churras que apresentará a tabela de participantes
já convidados. O valor de contribuição de churras e bebidas
deste participante será calculado e exibido em baixo da tabela,
que listará os seguintes valores:

- Valor total em churras 
- Valor total em bebidas
- Valor total arrecadado (churras + bebidas)
- Valor restante (valor total de contribuição em churras
e bebidas - valor total arrecadado)

[Tabela de participantes convidados (img)](https://i.imgur.com/NsPhj4n.png)

Apenas os participantes confirmados
terão os valores correspondentes calculados.

Nesta tabela de participantes será possível
a edição, visualização de detalhes e remoção
dos participantes.
