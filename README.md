# 🖥️ Aplicativo de Exibição de Senhas via WebSocket (Windows)

Este é um aplicativo desktop desenvolvido em **C#** para exibir, em tempo real, senhas chamadas de um servidor utilizando **WebSocket**. O projeto tem como objetivo principal facilitar a visualização de senhas em painéis ou totens, ideal para ambientes como clínicas, laboratórios, repartições públicas, entre outros.

---

## 🧰 Funcionalidades

- 📡 Conexão automática via WebSocket com servidor configurável.
- 🪄 Interface simples e objetiva para exibição de senhas.
- 🔊 Pode ser facilmente integrado com sistemas de chamada de senhas.
- 🔁 Atualização em tempo real sem necessidade de recarregar ou reiniciar a aplicação.

---

## 🛠️ Tecnologias Utilizadas

O projeto utiliza as seguintes bibliotecas e pacotes:

| Pacote                         | Versão    |
|-------------------------------|-----------|
| AForge                        | 2.2.5     |
| AForge.Video                  | 2.2.5     |
| AForge.Video.DirectShow       | 2.2.5     |
| Newtonsoft.Json               | 12.0.3    |
| System.Reactive               | 4.3.2     |
| System.Runtime.CompilerServices.Unsafe | 4.5.2 |
| System.Threading.Channels     | 4.7.0     |
| System.Threading.Tasks.Extensions | 4.5.3 |
| System.ValueTuple             | 4.5.0     |
| Websocket.Client              | 4.3.21    |

---

## ⚙️ Requisitos

- Windows 7 ou superior (Para Windows 7 é necessário instalar .NET Framework, versão indicada automaticamente durante a instalação)
- .NET Framework ou .NET Core/5+/6+, conforme compatibilidade com os pacotes (verifique no `csproj`)
- Permissão para acesso à internet/localhost (conforme o servidor WebSocket)

---

## 🧩 Como usar

1. **Clone este repositório**:

   ```bash
   git clone https://github.com/seu-usuario/nome-do-repositorio.git


2. **Abra o projeto no Visual Studio (ou outro editor compatível com C#).**

3. **Configure o endereço do WebSocket no arquivo de configuração ou no código (dependendo da implementação).**

4. **Compile e execute o projeto.**

5. **A aplicação se conectará ao servidor e exibirá as senhas chamadas em tempo real.**

## 🤝 Contribuição
Contribuições são bem-vindas! Sinta-se à vontade para abrir uma issue com sugestões, bugs ou melhorias, ou enviar um pull request.

## 📄 Licença
Este projeto está licenciado sob a MIT License.

## 📬 Contato
Se tiver dúvidas, sugestões ou quiser saber mais, entre em contato:

📧 Seu Email: swat2014@outlook.com

🧑‍💻 GitHub: @detonador31
