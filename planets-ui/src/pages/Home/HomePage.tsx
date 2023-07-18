import { Link, useLoaderData } from "react-router-dom";
import { HomePageDataLoaderResponse } from "./homePageDataLoader";

export function HomePage() {
  const { planets } = useLoaderData() as HomePageDataLoaderResponse;

  return (
    <div className="min-w-full min-h-full flex justify-center items-center text-center">
      <ul className="text-center">
        {planets.map((planet) => (
          <li
            key={planet.id}
            className="h-16 flex items-center justify-center text-4xl"
          >
            <Link
              to={`/${planet.id}`}
              className="relative outline-slate-400 before:content-[''] before:absolute before:block before:w-full before:h-[2px] before:bottom-0 before:left-0 before:bg-slate-400 before:transform before:scale-x-0 before:transition-transform before:duration-300 before:ease-in-out hover:before:transform hover:before:scale-x-100 focus-visible:before:transform focus-visible:before:scale-x-100"
            >
              {planet.name}
            </Link>
          </li>
        ))}
      </ul>
    </div>
  );
}
