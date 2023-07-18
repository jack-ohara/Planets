import { Link, useLoaderData } from "react-router-dom";
import { HomePageDataLoaderResponse } from "./homePageDataLoader";

export function HomePage() {
  const { planets } = useLoaderData() as HomePageDataLoaderResponse;

  return (
    <ul>
      {planets.map((planet) => (
        <li key={planet.id}>
          <Link to={`/${planet.id}`}>{planet.name}</Link>
        </li>
      ))}
    </ul>
  );
}
