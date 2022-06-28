
using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
namespace cmd
{
	class Program
	{
		static string activepath = Directory.GetCurrentDirectory() + '\\';

		public static void Main(string[] args)
		{
			Console.WriteLine("Для вывода списка всех команд и ключей к ним введите <<помощь>>");


			Console.Write(activepath + '>');
			string command = Console.ReadLine();

			string[] commandmas = command.Split(' ');
			GetCommand(commandmas);
			Console.ReadKey(true);
		}
		public static void GetCommand(string[] command)
		{

			switch (command[0])
			{
				case "вд":
					{
						if (command[1] == "/?")
						{
							Console.WriteLine("вд новыйпуть");
						}
						else
						{

							string path = "";
							for (int i = 1; i < command.Length; i++)
							{
								path += command[i] + " ";

							}
							path = path.Remove(path.Length - 1, 1);
							if (command[1] == ".." && command.GetLength(0) == 2)
							{
								int number = FindUpperDirectory(activepath);
								string t = activepath.Remove(number, activepath.Length - number) + '\\';

								if (t.Length > 2)
								{
									activepath = t;

								}
								else
								{

								}

							}
							else if (Directory.Exists(path))
							{
								DirectoryInfo dinfo = new DirectoryInfo(path);

								activepath = dinfo.FullName;

							}
							else
							{


								Console.WriteLine("Путь не найден");

							}

							if (activepath[activepath.Length - 1] != '\\')
							{
								activepath = activepath + '\\';
							}
							Directory.SetCurrentDirectory(activepath);


						}
						break;
					}
				case "очистить":
					{
						Console.Clear();
						break;
					}
				case "выход":
					{
						System.Environment.Exit(0);
						break;
					}
				case "дир":
					{

						var keylist = new List<string>() { "/ф", "/к" };
						var dinfo = new DirectoryInfo(activepath);
						string[] filenames = Directory.GetFiles(activepath);
						string[] directorynames = Directory.GetDirectories(activepath);

						if (command.Length == 2 && command[1] == "/?")
						{
							Console.WriteLine("дир [/ф]или[/п]" + '\n' + "Где ключ /ф - вывод только файлов" + '\n' + "/к - вывод только каталогов");
						}

						else if (command.Length == 1)
						{
							for (int i = 0; i < filenames.Length; i++)
							{

								Console.WriteLine("Файлы:" + filenames[i].Substring(activepath.LastIndexOf('\\')));


							}
							for (int i = 0; i < directorynames.Length; i++)
							{
								Console.WriteLine("Папки :" + directorynames[i].Substring(activepath.LastIndexOf('\\')));
							}
						}
						else if (command.Length == 2 && command[1] != "/?")
						{
							try
							{
								if (command[1] == "/ф")
								{
									for (int i = 0; i < filenames.Length; i++)
									{

										Console.WriteLine("Файлы:" + filenames[i].Substring(activepath.LastIndexOf('\\')));
										;
									}
								}
								if (command[1] == "/к")
								{
									for (int i = 0; i < directorynames.Length; i++)
									{
										Console.WriteLine("Папки :" + directorynames[i].Substring(activepath.LastIndexOf('\\')));
									}
								}
							}
							catch
							{



							}
						}

						break;
					}
				case "создфайл":
					{
						if (command[1] == "/?")
						{
							Console.WriteLine("создфайл путьдофайла/названиефайла");
						}
						else
						{


							try
							{
								string name = "";
								for (int i = 1; i < command.Length; i++)
								{
									name += command[i];
									name += ' ';
								}
								name = name.Remove(name.Length - 1, 1);
								FileInfo newfile = new FileInfo(name);

								newfile.Create();
							}
							catch
							{
								Console.WriteLine("Неправильно выбран путь для создания файла");
							}
						}
						break;
					}
				case "созддир":
					{
						if (command[1] == "/?")
						{
							Console.WriteLine("");
						}
						else
						{
							try
							{
								int pathindex = 0;
								
								string allstring = "";
								for (int i = 0; i < command.Length; i++)
								{
									allstring += command[i] + ' ';
								}
								allstring = allstring.Remove(allstring.Length - 1, 1);
								allstring = allstring.Remove(0, 8);

								for (int i = 0; i < allstring.Length; i++)
								{
									if (allstring[i] == ':')
									{
										pathindex = i;

									}

								}


								string path = allstring.Substring(pathindex - 1);

								string name = allstring.Substring(0, pathindex - 1);

								DirectoryInfo newdir = new DirectoryInfo(path + '\\' + name);
								newdir.Create();
							}
							catch
							{
								Console.WriteLine("Неправильно выбран путь для создания каталога");

							}
						}
						break;
					}
				case "удалфайл":
					{
						FileInfo delfile = new FileInfo(command[1]);
						delfile.Delete();
						break;
					}
				case "удалдир":
					{
						string name = "";
						if (command[1] != "/?")
						{
							for (int i = 1; i < command.Length; i++)
							{
								name += command[i];
								name += ' ';
							}
							DirectoryInfo deldir = new DirectoryInfo(name);
							if (Directory.Exists(name))
							{
								bool deleteing;
								Console.Write("Подтверждаете удаление? Удалятся также и подкаталоги. [Д-да/Н-нет]");
								string confirm = Console.ReadLine();
								if (confirm == "Д")
								{
									deleteing = true;
									deldir.Delete(deleteing);

								}
								else if (confirm == "Н")
								{
									deleteing = false;
								}
								else
								{
									Console.WriteLine("Удаление прекращено.");
								}
							}
							else
							{
								Console.WriteLine("Каталога не существует.");
								Console.WriteLine(name);
							}
						}
						else
						{
							Console.WriteLine("удалдир путьдокаталога");
						}
						break;
					}
				case "копир":
					{
						try
						{


							if (command.Length > 2)
							{
								int pathindex = 0;

								string allstring = "";
								for (int i = 0; i < command.Length; i++)
								{
									allstring += command[i] + ' ';
								}
								allstring = allstring.Remove(allstring.Length - 1, 1);
								allstring = allstring.Remove(0, 6);

								for (int i = 0; i < allstring.Length; i++)
								{
									if (allstring[i] == ':')
									{
										pathindex = i;

									}


								}

								string path = allstring.Substring(pathindex - 1);
								string name = allstring.Substring(0, pathindex - 1);

								bool file = false;
								for (int i = 0; i < name.Length; i++)
								{
									char c = name[i];
									if (Char.IsPunctuation(c))
									{
										file = true;
									}
								}
								if (file)
								{
									FileInfo newfile = new FileInfo(name);
									FileInfo delfile = new FileInfo(path + name);
									try
									{
										delfile.Delete();
										newfile.CopyTo(path + '\\' + name);
									}
									catch
									{
										Console.WriteLine("Неправильно указан путь.");
									}
								}




							}
							else if (command[1] == "/?")
							{
								Console.WriteLine("копир файл.расширение путь" + '\n');
							}
						}
						catch
						{
							Console.WriteLine("Ошибка");
						}
						break;
					}
				case "двиг":
					{
						try
						{
							if (command[1] == "/?")
							{
								Console.WriteLine("Пример : двиг файл/каталог каталог");
							}
							else
							{
								int pathindex = 0;

								string allstring = "";


								for (int i = 0; i < command.Length; i++)
								{
									allstring += command[i] + ' ';
								}
								allstring = allstring.Remove(0, 5);

								for (int i = 0; i < allstring.Length; i++)
								{
									if (allstring[i] == ':')
									{
										pathindex = i;

									}


								}

								string path = allstring.Substring(pathindex - 1);
								string name = allstring.Substring(0, pathindex - 1);
								bool file = false;
								for (int i = 0; i < name.Length; i++)
								{
									char c = name[i];
									if (Char.IsPunctuation(c))
									{
										file = true;
									}
								}
								if (file)
								{
									try
									{
										FileInfo newfile = new FileInfo(name);
										newfile.MoveTo(path + '\\' + name);
									}
									catch
									{

									}
								}
								if (!file)
								{
									try
									{
										DirectoryInfo newdir = new DirectoryInfo(name);
										newdir.MoveTo(path);
									}
									catch
									{

									}
								}
							}
						}
						catch
						{

						}
						break;
					}
				case "создбат":
					{

						if (command[1] == "/?")
						{
							Console.WriteLine("Создаёт файл .bat. " + '\n' + "создбат названиефайла");
						}
						else
						{
							string name = string.Empty;
							for (int i = 1; i < command.Length; i++)
							{
								name += command[i];
							}
							if (name == string.Empty)
							{
								name = "Безымянный";
							}
							if (File.Exists(name))
								File.Delete(name);
							Process.Start("notepad.exe", name + ".bat");
						}
						break;
						
					}
				case "эхо":
					{
						if (command.Length > 1)
						{
							string echo = "";
							for (int i = 1; i < command.Length; i++)
							{
								echo += command[i];

							}
							Console.WriteLine(echo);
						}
						if (command[1] == "/?")
						{
							Console.WriteLine("эхо текст" + '\n');

						}
						break;
					}
				case "помощь":
					{
						Console.WriteLine("Список команд:");
						Console.Write('\n' + "вд - смена текущего каталога" + '\n'
									  + "очистить - очищает консоль" + '\n'
									  + "выход - закрывает консоль" + '\n'
									  + "дир - выводит список каталогов и файлов в текущем каталоге" + '\n'
									  + "создфайл - создает файл" + '\n'
									  + "созддир - создает каталог" + '\n'
									  + "удалфайл - удаляет файл" + '\n'
									  + "удалдир - удаляет каталог" + '\n'
									  + "копир - копирует файл" + '\n'
									  + "двиг - перенести файл/каталог в другуой каталог" + '\n'
									  + "создбат - создает командный файл .bat" + '\n' + '\n'
									  + "для получения справки по каждой команде введите ее название и ключ /?" + '\n' + '\n'
										 );
						break;
					}
				default:
					{
						Console.WriteLine("Такой команды нет. Список доступных команд можно узнать командой <<помощь>>");
						break;
					}
			}
			Console.Write(activepath + '>');
			string commandd = Console.ReadLine();
			string[] commandmas = commandd.Split(' ');
			GetCommand(commandmas);

		}
		static int FindUpperDirectory(string path)
		{
			int slashpos = 0;

			int number = path.LastIndexOf('\\');
			for (int i = number - 1; i > 0; i--)
			{
				if (path[i] == '\\')
				{
					slashpos = i;
					break;
				}
			}
			return slashpos;
		}

	}
}